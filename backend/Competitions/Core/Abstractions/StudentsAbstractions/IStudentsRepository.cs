using Competitions.Core.Models;

namespace Competitions.Core.Abstractions.StudentsAbstractions
{
    public interface IStudentsRepository
    {
        Task<(Student? student, string error)> Create(Student student);
        Task<int> Delete(int id);
        Task<List<Student>> Get();
        Task<(Student? student, string error)> GetById(int id);
        Task<(Student?, string)> Update(int id, string name, string surname, DateTime dateOfBirth, int teamId);
    }
}