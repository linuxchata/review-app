using LC.RA.LocationService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.LocationService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SynchronizationController : Controller
    {
        private readonly ILocationSynchronizationService locationSynchronizationService;

        public SynchronizationController(ILocationSynchronizationService locationSynchronizationService)
        {
            this.locationSynchronizationService = locationSynchronizationService;
        }

        [HttpGet]
        public IActionResult SyncLocations()
        {
            this.locationSynchronizationService.Synchronize();

            return this.Ok();
        }
    }
}