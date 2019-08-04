using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Players.FirstOrDefault(a => a.Id == playerId);
        }

        public PagedList<Player> GetPlayers(int pageNumber, int pageSize){
            var playerColl = _context.Players
                .OrderBy(a => a.LastName)
                .OrderBy(a => a.FirstName)
                .AsQueryable();

            return PagedList<Player>.Create(playerColl,
                pageNumber,
                pageSize);    
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