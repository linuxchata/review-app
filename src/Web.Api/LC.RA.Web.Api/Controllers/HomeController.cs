using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
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
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Index()
        {
            return this.Ok("Web is up and running");
        }
    }
}