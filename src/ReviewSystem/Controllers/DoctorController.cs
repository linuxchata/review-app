using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewSystem.Core;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.doctorService.GetAllAsync();
            var entities = result.ToList();
            if (!entities.Any())
            {
                return this.NoContent();
            }

            return this.Ok(entities);
        }

        [HttpGet("{id}", Name = "GetDoctor")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            var entity = await this.doctorService.GetByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            return this.Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Doctor entity)
        {
            if (entity == null || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.doctorService.CreateAsync(entity);

            return this.CreatedAtRoute("GetDoctor", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]Doctor entity)
        {
            if (entity == null || entity.Id != id || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var existingEntity = await this.doctorService.GetByIdAsync(entity.Id);
            if (existingEntity == null)
            {
                return this.NotFound();
            }

            await this.doctorService.UpdateAsync(entity);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            var existingEntity = await this.doctorService.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return this.NotFound();
            }

            await this.doctorService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}
