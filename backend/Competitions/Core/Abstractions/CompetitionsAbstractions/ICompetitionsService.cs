using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.CompetitionsAbstractions
{
    public interface ICompetitionsService
    {
        Task<Result<List<Team>>> AddTeam(int id, int teamId);
        Task<Result<Competition>> CreateCompetition(Competition competition);
        Task<Result<int>> DeleteCompetition(int id);
        Task<Result<List<Team>>> DeleteTeam(int id, int teamId);
        Task<Result<List<Competition>>> GetAllCompetitions();
        Task<Result<Competition>> GetCompetitionById(int id);
        Task<Result<List<Team>>> GetTeamsOfCompetition(int id);
        Task<Result<Competition>> UpdateCompetition(int id, string name, string description, int kindOfSportId);
    }
}