using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IDraftSelectionRepository
    {
        bool LeagueDraftSelectionExists(Guid draftId, Guid teamId, int round );
        DraftSelection GetLeagueDraftSelection(Guid draftId, Guid teamId, int round);

        IEnumerable<DraftSelection> GetLeagueDraftSelectionsForLeague(Guid draftId);

        IEnumerable<DraftSelection> GetLeagueDraftSelectionsForTeam(Guid draftId, Guid teamId);

        void AddDraftSelectionToDraft(Guid leagueId, Guid draftId, DraftSelection draftSelection);

        void UpdateDraftSelectionToDraft(Guid draftId, DraftSelection draftSelection);

        
    }
}