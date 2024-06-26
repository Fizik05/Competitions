﻿using Competitions.Core.Abstractions.TeamsAbstractions;
using Competitions.Core.Models;
using CSharpFunctionalExtensions;

namespace Competitions.Application.Services
{
    public class TeamsService : ITeamsService
    {
        private ITeamsRepository _teamsRepository;
        public TeamsService(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public async Task<Result<List<Team>>> GetAllTeams()
        {
            return await _teamsRepository.Get();
        }

        public async Task<Result<Team>> GetTeamById(int id)
        {
            return await _teamsRepository.GetById(id);
        }

        public async Task<Result<List<Student>>> GetStudentOfTeam(int id)
        {
            return await _teamsRepository.GetStudents(id);
        }

        public async Task<Result<List<Competition>>> GetCompetitionsOfTeam(int id)
        {
            return await _teamsRepository.GetCompetitions(id);
        }

        public async Task<Result<List<Competition>>> AddCompetition(int id, int competitionId)
        {
            return await _teamsRepository.AddCompetition(id, competitionId);
        }

        public async Task<Result<List<Competition>>> DeleteCompetition(int id, int competitionId)
        {
            return await _teamsRepository.DeleteCompetition(id, competitionId);
        }

        public async Task<Result<Team>> CreateTeam(Team team)
        {
            return await _teamsRepository.Create(team);
        }

        public async Task<Result<Team>> UpdateTeam(
            int id,
            string name,
            int kindOfSportId,
            int universityId,
            int coachId)
        {
            return await _teamsRepository.Update(
                id,
                name,
                kindOfSportId,
                universityId,
                coachId);
        }

        public async Task<Result<int>> DeleteTeam(int id)
        {
            return await _teamsRepository.Delete(id);
        }
    }
}
