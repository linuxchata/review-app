using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
{
    /// <summary>
    /// Provides API for locations
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationService locationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class
        /// </summary>
        /// <param name="locationService">Location service</param>
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        /// <returns>List of all locations</returns>
        /// <response code="204">No locations were found</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Location>), 200)]
        [ProducesResponseType(204)]
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

        /// <summary>
        /// Get all locations for given search criteria
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>List of all locations for given search criteria</returns>
        /// <response code="400">Search criteria is null or empty</response>
        /// <response code="404">No locations were found</response>
        [HttpGet("{searchCriteria}")]
        [ProducesResponseType(typeof(List<Location>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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