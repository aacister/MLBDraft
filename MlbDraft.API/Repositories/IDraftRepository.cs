using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IDraftRepository
    {
        bool DraftExists(Guid Id);
        bool DraftExistsForLeague(Guid leagueId, Guid Id);
        Draft GetDraft(Guid draftId);

        Draft GetDraftForLeague(Guid leagueId, Guid draftId);

        IEnumerable<Draft> GetDrafts();

        IEnumerable<Draft> GetDraftsForLeague(Guid leagueId);

        void AddDraft(Draft draft);

        void AddDraftForLeague(Guid leagueId, Draft draft);

        void UpdateDraftForLeague(Guid leagueId, Draft draftToUpdate, Draft draft);

        void DeleteDraft(Draft draft);


        
    }
}