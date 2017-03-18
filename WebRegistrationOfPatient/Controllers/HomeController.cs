using Microsoft.AspNetCore.Mvc;

namespace WebRegistrationOfPatient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect(Url.Content("~/app/index.html"));
        }
    }
}