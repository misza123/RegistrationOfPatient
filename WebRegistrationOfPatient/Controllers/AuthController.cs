using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebRegistrationOfPatient.Core;
using WebRegistrationOfPatient.Models;
using WebRegistrationOfPatient.Models.Enums;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient.Controllers
{
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly TokenAuthOption _jwtOptions;
    private readonly ILogger _logger;
    private readonly IPatientRepository _patientRepository;
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly IUserRepository _userRepository;

    public AuthController(
      IOptions<TokenAuthOption> jwtOptions,
      ILoggerFactory logger,
      IUserRepository userRepository,
      IPatientRepository patientRepository)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);

      _logger = logger.CreateLogger<AuthController>();
      _userRepository = userRepository;
      _patientRepository = patientRepository;

      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented
      };
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromBody] AppUser usr)
    {
      var user = _userRepository.GetUserByLogin(usr.Login);
      if (user == null)
      {
        _logger.LogInformation($"Invalid username ({user.Login}) or password ({user.Password})");
        return NotFound("Invalid login or password.");
      }
      var identity = await GetClaimsIdentity(user);

      if (identity == null)
      {
        _logger.LogInformation($"Invalid credentials for user login: {user.Login}");
        return BadRequest("Invalid credentials");
      }

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Login),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst("Employee"),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst("Patient")
      };

      var jwt = new JwtSecurityToken(
        _jwtOptions.Issuer,
        _jwtOptions.Audience,
        claims,
        _jwtOptions.NotBefore,
        _jwtOptions.Expiration,
        _jwtOptions.SigningCredentials
      );

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      var userType = GetUserType(user);

      var response = new
      {
        access_token = encodedJwt,
        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
        userType
      };

      var json = JsonConvert.SerializeObject(response, _serializerSettings);

      return new OkObjectResult(json);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Register")]
    public ActionResult Register([FromBody] PatientDTO newPatient)
    {
      var patient = new Patient
      {
        Address = new Address
        {
          City = newPatient.Address.City,
          PostalCode = newPatient.Address.PostalCode,
          FlatNumber = newPatient.Address.FlatNumber,
          HouseNumber = newPatient.Address.HouseNumber,
          Street = newPatient.Address.Street
        },
        EmailAddress = newPatient.EmailAddress,
        PhoneNumber = newPatient.PhoneNumber,
        Name = newPatient.Name,
        Surname = newPatient.Surname,
        PersonalIdentityNumber = newPatient.PersonalIdentityNumber
      };

      var user = new User
      {
        Patient = patient,
        Login = patient.PersonalIdentityNumber,
        Password = patient.PersonalIdentityNumber
      };

      //todo add transaction, logger
      _patientRepository.Add(patient);
      _userRepository.Add(user);

      var response = new AppUser
      {
        Login = patient.PersonalIdentityNumber,
        Password = patient.PersonalIdentityNumber
      };

      var json = JsonConvert.SerializeObject(response, _serializerSettings);

      return new OkObjectResult(json);
    }

    private static UserType GetUserType(User user)
    {
      var userType = UserType.NoType;

      if (user.Employee != null && user.Patient != null)
      {
        userType = UserType.PatientAndEmployee;
      }
      if (user.Employee != null && user.Patient == null)
      {
        userType = UserType.Employee;
      }
      if (user.Employee == null && user.Patient != null)
      {
        userType = UserType.Patient;
      }
      return userType;
    }

    private static void ThrowIfInvalidOptions(TokenAuthOption options)
    {
      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenAuthOption.ValidFor));
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentNullException(nameof(TokenAuthOption.SigningCredentials));
      }

      if (options.JtiGenerator == null)
      {
        throw new ArgumentNullException(nameof(TokenAuthOption.JtiGenerator));
      }
    }

    /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
    private static long ToUnixEpochDate(DateTime date)
    {
      return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

    private Task<ClaimsIdentity> GetClaimsIdentity(User user)
    {
      //todo check password

      if (user == null)
      {
        return Task.FromResult<ClaimsIdentity>(null);
      }

      if (user.Employee != null && user.Patient != null)
      {
        return Task.FromResult(
          new ClaimsIdentity(
            new GenericIdentity(user.Login, "Token"),
            new[]
            {
              new Claim("Employee&Patient", $"{user.Employee.Name}.{user.Employee.Surname}")
            }));
      }

      if (user.Employee == null && user.Patient != null)
      {
        return Task.FromResult(
          new ClaimsIdentity(
            new GenericIdentity(user.Login, "Token"),
            new[]
            {
              new Claim("Patient", $"{user.Patient.Name}.{user.Patient.Surname}")
            }));
      }

      if (user.Employee != null && user.Patient == null)
      {
        return Task.FromResult(
          new ClaimsIdentity(
            new GenericIdentity(user.Login, "Token"),
            new[]
            {
              new Claim("Employee", $"{user.Employee.Name}.{user.Employee.Surname}")
            }));
      }

      return Task.FromResult<ClaimsIdentity>(null);
    }
  }

  public class AppUser
  {
    public string Login { get; set; }

    public string Password { get; set; }
  }
}
