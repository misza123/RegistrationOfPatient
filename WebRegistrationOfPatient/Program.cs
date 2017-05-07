using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebRegistrationOfPatient
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseKestrel()
        .UseStartup<Startup>()
        .UseApplicationInsights()
        .Build();

      host.Run();
    }
  }
}
