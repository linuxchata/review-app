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
            var result = await this.specializationService.GetAllAsync();
            var entities = result.ToList();
            if (!entities.Any())
            {
                return this.NoContent();
            }

            return this.Ok(entities);
        }

        [HttpGet("{searchCriteria}")]
        public async Task<IActionResult> GetBySearchCriteria(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                return this.BadRequest();
            }

            var result = await this.specializationService.GetBySearchCriteriaAsync(searchCriteria);
            var entities = result.ToList();
            if (!entities.Any())
            {
                return this.NotFound();
            }

            return this.Ok(entities);
        }
    }
}