using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Location.Api.Controllers
{
    /// <summary>
    /// Default controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>200 status code</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.Ok("Location service is up and running");
        }
    }
}