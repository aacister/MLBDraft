using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLBDraft.API.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private MLBDraftContext _context;
        
        public TeamRepository(MLBDraftContext context){
            _context = context;
        }

        public bool TeamExists(Guid teamId)
        {
            return _context.Teams.Any(a => a.Id == teamId);
        }

        public Team GetTeam(Guid teamId)
        {
            return _context.Teams.FirstOrDefault(a => a.Id == teamId);
        }
         public IEnumerable<Team> GetTeams(IEnumerable<Guid> teamIds)
        {
            return _context.Teams.Where(a => teamIds.Contains(a.Id))
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Team GetTeamForLeague(Guid leagueId, Guid teamId)
        {
            return _context.Teams
                .Where(t => t.LeagueId == leagueId && t.Id == teamId).FirstOrDefault();
        }

        public IEnumerable<Team> GetTeamsForLeague(Guid leagueId){
            return _context.Teams
                .Where(t => t.LeagueId == leagueId).OrderBy(t => t.Name).ToList();
        }

        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
        }
       
    }
}