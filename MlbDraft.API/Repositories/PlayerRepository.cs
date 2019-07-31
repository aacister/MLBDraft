using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Player> GetPlayers(){
            return _context.Players
                .OrderBy(a => a.LastName)
                .OrderBy(a => a.FirstName)
                .ToList();
        }
        public IEnumerable<Player> GetPlayers(IEnumerable<Guid> playerIds)
        {
            return _context.Players.Where(a => playerIds.Contains(a.Id))
                .OrderBy(a => a.LastName)
                .OrderBy(a => a.FirstName)
                .ToList();
        }

         public void AddPlayer(Player player)
        {
            player.Id = Guid.NewGuid();
            _context.Players.Add(player);

        }
    }
}