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
        public async Task<IActionResult> Get()
        {
            var result = await this.doctorService.GetAllAsync();
            var doctors = result.ToList();
            if (!doctors.Any())
            {
                return this.NoContent();
            }

            return this.Ok(doctors);
        }

        [HttpGet("{id}", Name = "GetDoctor")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            var doctor = await this.doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return this.NotFound();
            }

            return this.Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Doctor doctor)
        {
            if (doctor == null)
            {
                return this.BadRequest();
            }

            await this.doctorService.AddAsync(doctor);

            return this.Created("GetDoctor", doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody]Doctor doctor)
        {
            if (doctor == null || doctor.Id != id)
            {
                return this.BadRequest();
            }

            await this.doctorService.EditAsync(doctor);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            await this.doctorService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}
