using LC.RA.LocationService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.LocationService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SynchronizationController : Controller
    {
        private readonly ILocationService locationService;

        public SynchronizationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public IActionResult SyncLocations()
        {
            this.locationService.Synchronize();

            return this.Ok();
        }
    }
}