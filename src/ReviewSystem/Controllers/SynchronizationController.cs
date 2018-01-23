using Microsoft.AspNetCore.Mvc;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Controllers
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