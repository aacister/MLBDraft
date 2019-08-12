using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface ILeagueRepository
    {
        bool LeagueExists(Guid leagueId);

        bool LeagueNameExists(string leagueName);
        
        League GetLeague(Guid leagueId);

        IEnumerable<League> GetLeagues();

        void AddLeague(League league);

        void DeleteLeague(League league);
        
    }
}