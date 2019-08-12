using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface ITeamRepository
    {
        bool TeamExists(Guid teamId);

        bool TeamExistsForLeague(Guid leagueId, Guid teamId);
        
        bool TeamNameExists(string teamName);

        Team GetTeam(Guid teamId);

        IEnumerable<Team> GetTeams();

        Team GetTeamForLeague(Guid leagueId, Guid teamId);

        IEnumerable<Team> GetTeamsForLeague(Guid leagueId);

        void AddTeam(Team team);

        void AddTeamForLeague(Guid leagueId, Team team);

        void UpdateTeamForLeague(Guid leagueId, Team teamToUpdate, Team team);
        void DeleteTeam(Team team);
        
    }
}