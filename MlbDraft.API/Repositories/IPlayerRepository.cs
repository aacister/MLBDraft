using MLBDraft.API.Entities;
using MLBDraft.API.Helpers;
using System;
using System.Collections.Generic;


namespace MLBDraft.API.Repositories
{
    public interface IPlayerRepository
    {
        bool PlayerExists(Guid playerId);
        Player GetPlayer(Guid playerId);

        void AddPlayer(Player player);

        void DeletePlayer(Player player);

        PagedList<Player> GetPlayers(PlayerParameters playerParams);
 

    }
}