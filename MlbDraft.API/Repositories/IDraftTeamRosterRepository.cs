using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IDraftTeamRosterRepository
    {
        bool DraftTeamRosterExists(Guid id);

        bool DraftTeamRosterExists(Guid draftId, Guid teamId);

        DraftTeamRoster GetDraftTeamRoster(Guid id);

        DraftTeamRoster GetDraftTeamRoster(Guid draftId, Guid teamId);

        IEnumerable<DraftTeamRoster> GetDraftTeamRosters(Guid leagueId);

        void AddDraftTeamRoster(DraftTeamRoster roster);

        void AddTeamRosterToDraft(Guid draftId, DraftTeamRoster roster);

        void UpdateDraftTeamRoster( DraftTeamRoster roster);

        bool IsDraftTeamRosterPositionFilled(Guid rosterId, Guid positionId);

        
    }
}