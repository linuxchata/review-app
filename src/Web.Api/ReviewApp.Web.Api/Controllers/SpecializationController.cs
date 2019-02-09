using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.Services.Contracts;

namespace ReviewApp.Web.Api.Controllers
{
    /// <summary>
    /// Provides API for specializations
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService specializationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecializationController"/> class
        /// </summary>
        /// <param name="specializationService">Specialization service</param>
        public SpecializationController(ISpecializationService specializationService)
        {
            this.specializationService = specializationService;
        }

        /// <summary>
        /// Gets all specializations
        /// </summary>
        /// <returns>List of all specializations</returns>
        /// <response code="204">No specializations were found</response>
        /// <response code="200">List of all specializations</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<Specialization>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.specializationService.GetAllAsync();
            var specializations = result.ToList();
            if (!specializations.Any())
            {
                return this.NoContent();
            }

            return this.Ok(specializations);
        }

        /// <summary>
        /// Gets all specializations for given search criteria
        /// </summary>
        /// <param name="searchCriteria">Search criteria</param>
        /// <returns>List of all specializations for given search criteria</returns>
        /// <response code="400">Search criteria is null or empty</response>
        /// <response code="404">No specializations were found</response>
        /// <response code="200">List of all specializations for given search criteria</response>
        [HttpGet("{searchCriteria}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Specialization>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySearchCriteria(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                return this.BadRequest();
            }

            var result = await this.specializationService.GetBySearchCriteriaAsync(searchCriteria);
            var specializations = result.ToList();
            if (!specializations.Any())
            {
                return this.NotFound();
            }

            return this.Ok(specializations);
        }
    }
}