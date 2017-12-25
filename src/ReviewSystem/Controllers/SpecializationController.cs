using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Controllers
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
            var specializations = await this.specializationService.GetAllAsync();
            if (!specializations.Any())
            {
                return this.NoContent();
            }

            return this.Ok(specializations);
        }

        [HttpGet(Name = "GetBySearchCriteria")]
        public async Task<IActionResult> GetBySearchCriteria(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                return this.BadRequest();
            }

            var specializations = await this.specializationService.GetBySearchCriteriaAsync(searchCriteria);
            if (!specializations.Any())
            {
                return this.NotFound();
            }

            return this.Ok(specializations);
        }
    }
}