using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Core.Abstractions.CompetitionsAbstractions
{
    public interface ICompetitionsRepository
    {
        Task<Result<List<Team>>> AddTeam(int id, int teamId);
        Task<Result<Competition>> Create(Competition competition);
        Task<Result<int>> Delete(int id);
        Task<Result<List<Team>>> DeleteTeam(int id, int teamId);
        Task<Result<List<Competition>>> Get();
        Task<Result<Competition>> GetById(int id);
        Task<Result<List<Team>>> GetTeams(int id);
        Task<Result<Competition>> Update(int id, string name, string description, int kindOfSportId);
    }
}