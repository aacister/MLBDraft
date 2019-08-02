using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface ITeamRepository
    {
        bool TeamExists(Guid teamId);
        Team GetTeam(Guid teamId);

        IEnumerable<Team> GetTeams();

        IEnumerable<Team> GetTeams(IEnumerable<Guid> teamIds);

        Team GetTeamForLeague(Guid leagueId, Guid teamId);

        IEnumerable<Team> GetTeamsForLeague(Guid leagueId);

        void AddTeam(Team team);

        void AddTeamForLeague(Guid leagueId, Team team);

        void UpdateTeamForLeague(Team teamToUpdate, Team team);
        void DeleteTeam(Team team);
        
    }
}