using LC.RA.Location.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Location.Api.Controllers
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
            this.locationService.GetLocations();

            return this.Ok();
        }
    }
}