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
        [Route("GetAll")]
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

        [HttpGet(Name = "GetSpecializationBySearchCriteria")]
        [Route("GetSpecializationBySearchCriteria")]
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