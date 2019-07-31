using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IPlayerRepository
    {
        bool PlayerExists(Guid playerId);
        Player GetPlayer(Guid playerId);

        void AddPlayer(Player player);
        IEnumerable<Player> GetPlayers();
        IEnumerable<Player> GetPlayers(IEnumerable<Guid> playerIds);

    }
}