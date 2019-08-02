using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private MLBDraftContext _context;
        
        public LeagueRepository(MLBDraftContext context){
            _context = context;
        }

        public bool LeagueExists(Guid leagueId)
        {
            return _context.Leagues.Any(a => a.Id == leagueId);
        }

        public League GetLeague(Guid leagueId)
        {
            return _context.Leagues
            .Include(league => league.Teams)
            .FirstOrDefault(a => a.Id == leagueId);
        }

        public IEnumerable<League> GetLeagues(){
            return _context.Leagues
                .Include(league => league.Teams)
                .OrderBy(a => a.Name)
                .ToList();
        }

         public IEnumerable<League> GetLeagues(IEnumerable<Guid> leagueIds)
        {
            return _context.Leagues.
                Where(a => leagueIds.Contains(a.Id))
                .Include(league => league.Teams)
                .OrderBy(a => a.Name)
                .ToList();
        }

         public void AddLeague(League league)
        {
            league.Id = Guid.NewGuid();
            _context.Leagues.Add(league);

             if (league.Teams.Any())
            {
                foreach (var team in league.Teams)
                {
                    team.Id = Guid.NewGuid();
                }
            }
        }

        public void DeleteLeague(League league)
        {
            _context.Leagues.Remove(league);
        }
       
    }
}