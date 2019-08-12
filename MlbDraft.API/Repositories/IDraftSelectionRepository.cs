using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IDraftSelectionRepository
    {
        bool LeagueDraftSelectionExists(Guid leagueId, Guid draftId, Guid teamId, int round );
        DraftSelection GetLeagueDraftSelection(Guid leagueId, Guid draftId, Guid teamId, int round);

        IEnumerable<DraftSelection> GetLeagueDraftSelectionsForLeague(Guid leagueId, Guid draftId);

        IEnumerable<DraftSelection> GetLeagueDraftSelectionsForTeam(Guid leagueId, Guid draftId, Guid teamId);

        void AddDraftSelectionToDraft(Guid leagueId, Guid draftId, DraftSelection draftSelection);

        
    }
}