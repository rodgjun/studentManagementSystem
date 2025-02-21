using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Services;
using WebApiMongoDB.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiMongoDB.Server.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentServices _studentServices;

        public StudentController(StudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        // GET: api/student
        [HttpGet]
        public async Task<List<Student>> Get() =>
            await _studentServices.GetAsync();

        // GET api/student/67b749ffd26c17c2eefd217c
        [HttpGet("{id}length(24)")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            Student student = await _studentServices.GetAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return student;
        }

        // POST api/student
        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student newStudent)
        {
            await _studentServices.CreateAsync(newStudent);
            return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent); 
        }

        // PUT api/student/67b749ffd26c17c2eefd217c
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, Student updateStudent)
        {
            Student student = await _studentServices.GetAsync(id);
            if(student == null)
            {
                return NotFound("There is no student with this id" + id);
            }
            updateStudent.Id = student.Id;
            await _studentServices.UpdateAsync(id, updateStudent);

            return Ok("Updated Successfully");
        }

        // DELETE api/student/67b749ffd26c17c2eefd217c
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            Student student = await _studentServices.GetAsync(id);
            if (student == null)
            {
                return NotFound("There is no student with this id" + id);
            }

            await _studentServices.RemoveAsync(id);

            return Ok("Deleted Successfully");
        }
    }
}
