using LC.RA.WebApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SyncController : Controller
    {
        private readonly ILocationService locationService;

        public SyncController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public IActionResult SyncLocations()
        {
            this.locationService.RequestSynchronization();

            return this.Ok();
        }
    }
}