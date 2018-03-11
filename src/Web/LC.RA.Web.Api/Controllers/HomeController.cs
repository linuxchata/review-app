using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.Ok("Web is up and running");
        }
    }
}