using Competitions.Application.Services;
using Competitions.Contracts.Coaches;
using Competitions.Contracts.Students;
using Competitions.Core.Models;
using Competitions.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace Competitions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController: ControllerBase
    {
        private readonly StudentsService _studentsService;

        public StudentsController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentsResponse>>> GetAllStudents()
        {
            var students = await _studentsService.GetAllStudents();

            var studentsRespone = students
                .Select(s => new StudentsResponse(s.Id, s.Name, s.Surname, s.DateOfBirth, s.Team))
                .ToList();

            return Ok(studentsRespone);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentsResponse>> GetStudentById(int id)
        {
            var (student, error) = await _studentsService.GetStudentById(id);

            if (!string.IsNullOrEmpty(error) || student is null)
            {
                return BadRequest(error);
            }

            var response = new StudentsResponse(student.Id, student.Name, student.Surname, student.DateOfBirth, student.Team);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<StudentsResponse>> CreateStudent([FromBody] StudentRequest request)
        {
            var (student, error) = Student.Create(
                0,
                request.Name,
                request.Surname,
                request.DateOfBirth,
                request.TeamId,
                null);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error + "first");
            }

            var (createdStudent, error2)= await _studentsService.CreateStudent(student);

            if (!string.IsNullOrEmpty(error2) || createdStudent is null)
            {
                return BadRequest(error2 + "second");
            }
            
            var responseStudent = new StudentsResponse(createdStudent.Id, createdStudent.Name, createdStudent.Surname, createdStudent.DateOfBirth, createdStudent.Team);

            return Ok(responseStudent);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<StudentsResponse>> UpdateStudent(int id, [FromBody] StudentRequest request)
        {
            var (student, error) = await _studentsService.UpdateStudent(
                id,
                request.Name,
                request.Surname,
                request.DateOfBirth,
                request.TeamId);

            if (!string.IsNullOrEmpty(error) || student is null)
            {
                return BadRequest(error);
            }

            var responseStudent = new StudentsResponse(student.Id, student.Name, student.Surname, student.DateOfBirth, student.Team);

            return Ok(responseStudent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteStudent(int id)
        {
            var studentId = await _studentsService.DeleteStudent(id);

            return studentId;
        }
    }
}
