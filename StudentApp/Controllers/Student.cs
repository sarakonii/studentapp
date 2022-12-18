using System;
using Microsoft.AspNetCore.Mvc;


//Modeli Studentit
public class Student : BaseClass
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int GraduationYear { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime ModifiedDate { get; set; }
}

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        static List<Student> Students = new List<Student>
        {
            new Student
            {
                Id = 1,
                DateOfBirth = DateTime.Today.AddYears(-70),
                FirstName = "Miri",
                LastName = "Fisteku",
                GraduationYear = 1996,
                DateCreated = DateTime.Today,
                DateUpdated = DateTime.Today,
            },
            new Student
            {
                Id = 2,
                DateCreated = DateTime.Today,
                FirstName = "Filan",
                LastName = "Fisteku",
                GraduationYear = 2012,
                DateUpdated = DateTime.Today,
                DateOfBirth = DateTime.Today.AddYears(-50),
            }
        };

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            student.Id = Students.Count + 1;
            student.DateCreated = DateTime.Today;

            Students.Add(student);
            return Ok("Studenti u shtua");
        }

        [HttpPut]
        public IActionResult Put([FromBody] Student student)
        {
            if (student.Id > Students.Count)
                return NotFound("Studenti nuk u gjend!");

            if (student.DateUpdated != student.DateCreated)
                return NotFound("Studenti nuk mund të përditësohet");

            student.DateUpdated = DateTime.Today;
            Students[student.Id - 1] = student;
            return Ok("Studenti u përditësua");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == default)
                return NotFound("Studenti nuk u gjend!");

            Students.Remove(student);
            return Ok($"Studentët e mbetur: {Students.Count}");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(Students.FirstOrDefault(s => s.Id == id)!);
    }
}
