using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Location.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.Ok("Location service is up and running");
        }
    }
}