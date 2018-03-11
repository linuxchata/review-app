using System.Linq;
using System.Threading.Tasks;
using LC.RA.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LC.RA.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            this.specializationService = specializationService;
        }

        [HttpGet]
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

        [HttpGet("{searchCriteria}")]
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