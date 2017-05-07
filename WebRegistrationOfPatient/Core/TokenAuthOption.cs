using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebRegistrationOfPatient.Core
{
  public class TokenAuthOption
  {
    public string Audience { get; set; }

    public string Issuer { get; set; }

    public Func<Task<string>> JtiGenerator { get; } = () => Task.FromResult(Guid.NewGuid().ToString());

    public DateTime IssuedAt { get; } = DateTime.UtcNow;

    public DateTime Expiration => IssuedAt.Add(ValidFor);

    public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(30);

    public DateTime NotBefore => DateTime.UtcNow;

    public SigningCredentials SigningCredentials { get; set; }
  }
}
