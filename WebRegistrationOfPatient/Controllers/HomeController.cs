using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebRegistrationOfPatient.Controllers
{
  public class HomeController : Controller
  {
    [AllowAnonymous]
    public IActionResult Index()
    {
      return Redirect(Url.Content("~/app/index.html"));
    }
  }
}
