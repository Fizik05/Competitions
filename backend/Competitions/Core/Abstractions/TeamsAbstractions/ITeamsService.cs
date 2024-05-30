using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.TeamsAbstractions
{
    public interface ITeamsService
    {
        Task<Result<List<Competition>>> AddCompetition(int id, int competitionId);
        Task<Result<Team>> CreateTeam(Team team);
        Task<Result<List<Competition>>> DeleteCompetition(int id, int competitionId);
        Task<Result<int>> DeleteTeam(int id);
        Task<Result<List<Team>>> GetAllTeams();
        Task<Result<List<Competition>>> GetCompetitionsOfTeam(int id);
        Task<Result<List<Student>>> GetStudentOfTeam(int id);
        Task<Result<Team>> GetTeamById(int id);
        Task<Result<Team>> UpdateTeam(int id, string name, int kindOfSportId, int universityId, int coachId);
    }
}