using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.StudentsAbstractions
{
    public interface IStudentsService
    {
        Task<(Student?, string)> CreateStudent(Student student);
        Task<int> DeleteStudent(int id);
        Task<List<Student>> GetAllStudents();
        Task<(Student?, string)> GetStudentById(int id);
        Task<(Student?, string)> UpdateStudent(int id, string name, string surname, DateTime dateOfBirth, int teamId);
    }
}