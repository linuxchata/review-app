using System.Threading.Tasks;
using LC.RA.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
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
        public async Task<IActionResult> SyncLocations()
        {
            await this.locationService.RequestSynchronization();

            return this.Ok();
        }
    }
}