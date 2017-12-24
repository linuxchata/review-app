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
        public async Task<IActionResult> Get()
        {
            var subjects = await this.subjectService.GetAllAsync();
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
        public async Task<IActionResult> Add([FromBody]Subject subject)
        {
            if (subject == null)
            {
                return this.BadRequest();
            }

            await this.subjectService.AddAsync(subject);

            return this.Created("GetSubject", subject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody]Subject subject)
        {
            if (subject == null || subject.Id != id)
            {
                return this.BadRequest();
            }

            await this.subjectService.EditAsync(subject);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest();
            }

            await this.subjectService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}
