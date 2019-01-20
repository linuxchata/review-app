using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.Location.Api.Controllers
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            string runtime = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();
            return this.Ok($"Location API is up and running. OS is {runtime}");
        }
    }
}