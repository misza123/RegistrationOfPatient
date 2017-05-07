using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebRegistrationOfPatient.Core;
using WebRegistrationOfPatient.Repositories;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient
{
  public class Startup
  {
    //todo get from environment
    private const string SecretKey = "secretKeyNeedToGetFromEnv";

    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();

      services.AddDbContext<WebRegistrationContext>();

      // Add framework services.
      services.AddMvc(
        config =>
        {
          var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
          config.Filters.Add(new AuthorizeFilter(policy));
        });

      services.AddSingleton<IPatientRepository, PatientRepository>();
      services.AddSingleton<IUserRepository, UserRepository>();

      WebRegistrationOfPatient.Configuration.DefaultConnection = Configuration.GetConnectionString("DefaultConnection");

      services.AddAuthorization(
        options =>
        {
          options.AddPolicy("Employee", policy => policy.RequireClaim("Employee"));
          options.AddPolicy("Patient", policy => policy.RequireClaim("Patient"));
        });

      var jwtAppSettingOptions = Configuration.GetSection(nameof(TokenAuthOption));

      services.Configure<TokenAuthOption>(
        options =>
        {
          options.Issuer = jwtAppSettingOptions[nameof(TokenAuthOption.Issuer)];
          options.Audience = jwtAppSettingOptions[nameof(TokenAuthOption.Audience)];
          options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        });

      //services.Configure<Configuration>(Configuration.GetSection("ConnectionStrings"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      //loggerFactory.AddProvider();
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      var jwtAppSettingsOptions = Configuration.GetSection(nameof(TokenAuthOption));
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingsOptions[nameof(TokenAuthOption.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingsOptions[nameof(TokenAuthOption.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,

        RequireExpirationTime = true,
        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero
      };

      app.UseJwtBearerAuthentication(
        new JwtBearerOptions
        {
          AutomaticAuthenticate = true,
          AutomaticChallenge = true,
          TokenValidationParameters = tokenValidationParameters
        });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(
        routes =>
        {
          routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
          routes.MapRoute("homePage", "{*url}", new { controller = "Home", action = "Index" });
        });
    }
  }
}
