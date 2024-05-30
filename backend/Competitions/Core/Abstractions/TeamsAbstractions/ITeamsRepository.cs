using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.TeamsAbstractions
{
    public interface ITeamsRepository
    {
        Task<Result<List<Competition>>> AddCompetition(int id, int competitionId);
        Task<Result<Team>> Create(Team team);
        Task<Result<int>> Delete(int id);
        Task<Result<List<Competition>>> DeleteCompetition(int id, int competitionId);
        Task<Result<List<Team>>> Get();
        Task<Result<Team>> GetById(int id);
        Task<Result<List<Competition>>> GetCompetitions(int id);
        Task<Result<List<Student>>> GetStudents(int id);
        Task<Result<Team>> Update(int id, string name, int kindOfSportId, int universityId, int coachId);
    }
}