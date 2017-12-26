using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.locationService.GetAllAsync();
            var locations = result.ToList();
            if (!locations.Any())
            {
                return this.NoContent();
            }

            return this.Ok(locations);
        }

        [HttpGet(Name = "GetLocationBySearchCriteria")]
        [Route("GetLocationBySearchCriteria")]
        public async Task<IActionResult> GetBySearchCriteria(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                return this.BadRequest();
            }

            var result = await this.locationService.GetBySearchCriteriaAsync(searchCriteria);
            var locations = result.ToList();
            if (!locations.Any())
            {
                return this.NotFound();
            }

            return this.Ok(locations);
        }
    }
}