using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewSystem.Core;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Controllers
{
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this.subjectService.GetAllAsync();
            var subjects = result.ToList();
            if (!subjects.Any())
            {
                return this.NoContent();
            }

            return this.Ok(subjects);
        }

        [HttpGet("{id}", Name = "GetSubject")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            var subject = await this.subjectService.GetByIdAsync(id);
            if (subject == null)
            {
                return this.NotFound();
            }

            return this.Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Doctor subject)
        {
            if (subject == null || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.subjectService.CreateAsync(subject);

            return this.CreatedAtRoute("GetSubject", new { id = subject.Id }, subject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]Doctor subject)
        {
            if (subject == null || subject.Id != id || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var existingSubject = await this.subjectService.GetByIdAsync(subject.Id);
            if (existingSubject == null)
            {
                return this.NotFound();
            }

            await this.subjectService.UpdateAsync(subject);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            var existingSubject = await this.subjectService.GetByIdAsync(id);
            if (existingSubject == null)
            {
                return this.NotFound();
            }

            await this.subjectService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}
