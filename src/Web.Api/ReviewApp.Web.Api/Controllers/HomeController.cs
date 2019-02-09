using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewApp.Web.Api.Controllers
{
    /// <summary>
    /// Default controller
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>200 status code</returns>
        /// <response code="200">Service status and runtime</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            string runtime = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();
            return this.Ok($"Web API is up and running. OS is {runtime}");
        }
    }
}