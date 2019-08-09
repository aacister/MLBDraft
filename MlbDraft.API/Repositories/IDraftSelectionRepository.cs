using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IDraftSelectionRepository
    {
        bool DraftSelectionExists(Guid draftId, Guid teamId, int round, int selectionNo );
        DraftSelection GetDraftSelection(Guid draftId, Guid teamId, int round, int selectionNo);

        IEnumerable<DraftSelection> GetDraftSelections(Guid draftId);

        IEnumerable<DraftSelection> GetDraftSelectionsForTeam(Guid draftId, Guid teamId);

        void AddDraftSelectionToDraft(Guid draftId, DraftSelection draftSelection);

        
    }
}