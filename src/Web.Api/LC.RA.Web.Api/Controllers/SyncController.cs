using System.Threading.Tasks;
using LC.RA.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
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
        /// Trigger locations synchronization process
        /// </summary>
        /// <returns>200 status code</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SyncLocations()
        {
            await this.locationService.RequestSynchronization();

            return this.Ok();
        }
    }
}