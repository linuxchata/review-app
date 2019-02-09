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
    /// Provides API for subjects
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectController"/> class
        /// </summary>
        /// <param name="subjectService"></param>
        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        /// <summary>
        /// Gets all subjects
        /// </summary>
        /// <returns>List of all subjects</returns>
        /// <response code="204">No subjects were found</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Subject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        /// <summary>
        /// Gets subject by id
        /// </summary>
        /// <param name="id">The identifier of subject</param>
        /// <returns>Subject for given id</returns>
        [HttpGet("{id}", Name = "GetSubject")]
        [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Creates subject
        /// </summary>
        /// <param name="subject">The identifier of subject</param>
        /// <returns>201 status code</returns>
        /// <response code="400">Subject is null or invalid</response>
        /// <response code="209">Subject with the same name has already been created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody]Doctor subject)
        {
            if (subject == null || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var doesSubjectExist = await this.subjectService.ExistsAsync(subject);
            if (doesSubjectExist)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, "Subject with the same name has already been created");
            }

            await this.subjectService.CreateAsync(subject);

            return this.CreatedAtRoute("GetSubject", new { id = subject.Id }, subject);
        }

        /// <summary>
        /// Updates subject
        /// </summary>
        /// <param name="id">The identifier of subject</param>
        /// <param name="subject">The subject</param>
        /// <returns>204 status code</returns>
        /// <response code="400">Identifier is null or empty</response>
        /// <response code="404">No subject was found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        /// <summary>
        /// Deletes subject
        /// </summary>
        /// <param name="id">The identifier of subject</param>
        /// <returns>204 status code</returns>
        /// <response code="400">Identifier is null or empty</response>
        /// <response code="404">No subject was found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
