using Competitions.Core.Models;

namespace Competitions.DataAccess.Repositories
{
    public class StudentsService
    {
        private StudentsRepository _studentsRepository;

        public StudentsService(StudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentsRepository.Get();
        }

        public async Task<(Student?, string)> GetStudentById(int id)
        {
            return await _studentsRepository.GetById(id);
        }

        public async Task<(Student?, string)> CreateStudent(Student student)
        {
            return await _studentsRepository.Create(student);
        }

        public async Task<(Student?, string)> UpdateStudent(int id, string name, string surname, DateTime dateOfBirth, int teamId)
        {
            return await _studentsRepository.Update(id, name, surname, dateOfBirth, teamId);
        }

        public async Task<int> DeleteStudent(int id)
        {
            return await _studentsRepository.Delete(id);
        }
    }
}
