using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public bool TeamExistsForLeague(Guid leagueId, Guid teamId)
        {
            return _context.Teams
                .Any(t => t.League.Id == leagueId 
                && t.Id == teamId);
               
        }

        public  bool TeamNameExists(string teamName)
        {
            return _context.Teams
                .Any(t => t.Name.ToLowerInvariant() == teamName.ToLowerInvariant());
        }

        public Team GetTeam(Guid teamId)
        {
            return _context.Teams
            .Include(t => t.League)
            .Include(t => t.Owner)
            .FirstOrDefault(a => a.Id == teamId);
        }

         public IEnumerable<Team> GetTeams(){
            return _context.Teams
                .Include(t => t.League)
                .Include(t => t.Owner)
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Team GetTeamForLeague(Guid leagueId, Guid teamId)
        {
            return _context.Teams
                .Where(t => t.LeagueId == leagueId && t.Id == teamId)
                .Include(t => t.League)
                .Include(t => t.Owner)
                .FirstOrDefault();
        }

        public IEnumerable<Team> GetTeamsForLeague(Guid leagueId){
            return _context.Teams
                .Where(t => t.LeagueId == leagueId)
                .Include(t => t.League)
                .Include(t => t.Owner)              
                .OrderBy(t => t.Name).ToList();
        }

        public void AddTeam(Team team)
        {
            team.Id = Guid.NewGuid();
            _context.Teams.Add(team);

        }

        public void AddTeamForLeague(Guid leagueId, Team team)
        {
            var league = _context.Leagues
            .Where(l => l.Id == leagueId)
            .FirstOrDefault();

            if(league != null)
            {
                team.Id = Guid.NewGuid();
                league.Teams.Add(team);
            }
        }



        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
        }
       
    }
}