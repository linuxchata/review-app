using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ReviewApp.Web.Services.Contracts;

namespace ReviewApp.Web.Api.Controllers
{
    /// <summary>
    /// Provides API for synchronization
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SyncController : Controller
    {
        private readonly ILocationService locationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncController"/> class
        /// </summary>
        /// <param name="locationService"></param>
        public SyncController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Triggers locations synchronization process
        /// </summary>
        /// <returns>200 status code</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SyncLocations()
        {
            await this.locationService.RequestSynchronization();

            return this.Ok();
        }
    }
}