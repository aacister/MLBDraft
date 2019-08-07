using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MLBDraft.API.Helpers;

namespace MLBDraft.API.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private MLBDraftContext _context;
        
        public PlayerRepository(MLBDraftContext context){
            _context = context;
        }

         public bool PlayerExists(Guid playerId)
        {
            return _context.Players.Any(a => a.Id == playerId);
        }

        public Player GetPlayer(Guid playerId)
        {
            return _context.Players
            .Include(p => p.MlbTeam)
            .Include(p => p.Position)
            .Include(p => p.StatCategories)
            .FirstOrDefault(a => a.Id == playerId);
        }

        public PagedList<Player> GetPlayers(PlayerParameters playerParams){
            var playerColl = _context.Players
                .Include(p => p.MlbTeam)
                .Include(p => p.Position)
                .Include(p => p.StatCategories)
                .ThenInclude(p => p.StatCategory)
                .OrderBy(a => a.LastName)
                .OrderBy(a => a.FirstName)
                .AsQueryable();

            //Filter by  position using abbrev
            if(!string.IsNullOrEmpty(playerParams.Position))
            {
                var positionFilter = playerParams.Position.Trim().ToLowerInvariant();
                playerColl = playerColl
                    .Where(p => p.Position.Abbreviation.ToLowerInvariant() == positionFilter);
            }

            //Filter by MlbTeam using abbrev
            if(!string.IsNullOrEmpty(playerParams.Team))
            {
                var mlbTeamFilter = playerParams.Team.Trim().ToLowerInvariant();
                playerColl = playerColl
                    .Where(p => p.MlbTeam.Abbreviation.ToLowerInvariant() == mlbTeamFilter);
            }


            return PagedList<Player>.Create(playerColl,
                playerParams.PageNumber,
                playerParams.PageSize);    
        }

         public void AddPlayer(Player player)
        {
            player.Id = Guid.NewGuid();
            _context.Players.Add(player);

        }

        public void DeletePlayer(Player player)
        {

            _context.Players.Remove(player);
           
        }
    }
}