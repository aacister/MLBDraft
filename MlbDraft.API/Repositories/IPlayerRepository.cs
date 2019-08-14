using MLBDraft.API.Entities;
using MLBDraft.API.Helpers;
using System;
using System.Collections.Generic;


namespace MLBDraft.API.Repositories
{
    public interface IPlayerRepository
    {
        bool PlayerExists(Guid playerId);

        int GetPlayerCount();

        Player GetPlayer(Guid playerId);

        void AddPlayer(Player player);

        void DeletePlayer(Player player);

        IEnumerable<Player> GetPlayers();

        PagedList<Player> GetPlayers(PlayerParameters playerParams);
 

    }
}