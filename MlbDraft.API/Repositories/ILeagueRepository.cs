using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface ILeagueRepository
    {
        bool LeagueExists(Guid leagueId);
        League GetLeague(Guid leagueId);

        IEnumerable<League> GetLeagues();

        IEnumerable<League> GetLeagues(IEnumerable<Guid> leagueIds);

        void AddLeague(League league);

        void DeleteLeague(League league);
        
    }
}