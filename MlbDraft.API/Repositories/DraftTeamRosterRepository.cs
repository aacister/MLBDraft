using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class DraftTeamRosterRepository : IDraftTeamRosterRepository
    {
        private MLBDraftContext _context;
        
        public DraftTeamRosterRepository(MLBDraftContext context){
            _context = context;
        }

        public bool DraftTeamRosterExists(Guid id){
            return _context.DraftTeamRosters.Any(r => r.Id == id);
        }

        public bool DraftTeamRosterExists(Guid draftId, Guid teamId)
        {
            return _context.DraftTeamRosters.Any(r => r.DraftId == draftId && r.TeamId == teamId);
        }

        public DraftTeamRoster GetDraftTeamRoster(Guid id){
            return _context.DraftTeamRosters
            .Include(t => t.Draft)
            .Include(t => t.Team)
            .Include(t => t.Catcher)
            .Include(t => t.FirstBase)
            .Include(t => t.SecondBase)
            .Include(t => t.ShortStop)
            .Include(t => t.ThirdBase)
            .Include(t => t.Outfield1)
            .Include(t => t.Outfield2)
            .Include(t => t.Outfield3)
            .Include(t => t.StartingPitcher)
            .FirstOrDefault(a => a.Id == id);
        }

        public DraftTeamRoster GetDraftTeamRoster(Guid draftId, Guid teamId){
            return _context.DraftTeamRosters
            .Include(t => t.Draft)
            .Include(t => t.Team)
            .Include(t => t.Catcher)
            .Include(t => t.FirstBase)
            .Include(t => t.SecondBase)
            .Include(t => t.ShortStop)
            .Include(t => t.ThirdBase)
            .Include(t => t.Outfield1)
            .Include(t => t.Outfield2)
            .Include(t => t.Outfield3)
            .Include(t => t.StartingPitcher)
            .FirstOrDefault(a => a.DraftId == draftId && a.TeamId == teamId);
        }

         public IEnumerable<DraftTeamRoster> GetDraftTeamRosters(Guid leagueId){
            return _context.DraftTeamRosters
            .Include(t => t.Draft)
            .Include(t => t.Team)
            .Include(t => t.Catcher)
            .Include(t => t.FirstBase)
            .Include(t => t.SecondBase)
            .Include(t => t.ShortStop)
            .Include(t => t.ThirdBase)
            .Include(t => t.Outfield1)
            .Include(t => t.Outfield2)
            .Include(t => t.Outfield3)
            .Include(t => t.StartingPitcher)
            .Where(t => t.Draft.LeagueId == leagueId)
                .ToList();
        }

        public void AddDraftTeamRoster(DraftTeamRoster roster){
            roster.Id = Guid.NewGuid();
            _context.DraftTeamRosters.Add(roster);
        }

        public void AddTeamRosterToDraft(Guid draftId, DraftTeamRoster roster){

            var draft = _context.Drafts
            .Where(d => d.Id == draftId)
            .FirstOrDefault();

            if(draft != null)
            {
                draft.TeamRosters.Add(roster);
            }

        }

        public void UpdateDraftTeamRoster( DraftTeamRoster roster)
        {

            var draftRosterToEdit = _context.DraftTeamRosters
                .Where(d => d.DraftId == roster.DraftId
                && d.TeamId == roster.TeamId )
                .FirstOrDefault();

            if(draftRosterToEdit != null)
            {
                draftRosterToEdit.CatcherId = roster.CatcherId;
                draftRosterToEdit.FirstBaseId = roster.FirstBaseId;
                draftRosterToEdit.SecondBaseId = roster.SecondBaseId;
                draftRosterToEdit.ShortStopId = roster.ShortStopId;
                draftRosterToEdit.ThirdBaseId = roster.ThirdBaseId;
                draftRosterToEdit.Outfield1Id = roster.Outfield1Id;
                draftRosterToEdit.Outfield2Id = roster.Outfield2Id;
                draftRosterToEdit.Outfield3Id = roster.Outfield3Id;
                draftRosterToEdit.StartingPitcherId = roster.StartingPitcherId;
            }
        }

        public bool IsDraftTeamRosterPositionFilled(Guid rosterId, Guid positionId)
        {
            var roster = _context.DraftTeamRosters
                .Include(t => t.Catcher)
            .Include(t => t.FirstBase)
                .ThenInclude(t => t.Position)
            .Include(t => t.SecondBase)
                .ThenInclude(t => t.Position)
            .Include(t => t.ShortStop)
                .ThenInclude(t => t.Position)
            .Include(t => t.ThirdBase)
                .ThenInclude(t => t.Position)
            .Include(t => t.Outfield1)
                .ThenInclude(t => t.Position)
            .Include(t => t.Outfield2)
                .ThenInclude(t => t.Position)
            .Include(t => t.Outfield3)
                .ThenInclude(t => t.Position)
            .Include(t => t.StartingPitcher)
                .ThenInclude(t => t.Position)
            .Where(t => t.Id == rosterId)
            .FirstOrDefault();

            if(roster != null){
                var playersList = new List<Player>();
                if(roster.Outfield1 != null)
                    playersList.Add(roster.Outfield1);
                if(roster.Outfield2 != null)
                    playersList.Add(roster.Outfield2);
                if(roster.Outfield3 != null)
                    playersList.Add(roster.Outfield3);
                if(roster.Catcher != null)
                    playersList.Add(roster.Catcher);
                if(roster.FirstBase != null)
                    playersList.Add(roster.FirstBase);
                if(roster.SecondBase != null)
                    playersList.Add(roster.SecondBase);
                if(roster.ShortStop != null)
                    playersList.Add(roster.ShortStop);
                if(roster.ThirdBase != null)
                    playersList.Add(roster.ThirdBase);
                if(roster.StartingPitcher != null)
                    playersList.Add(roster.StartingPitcher);

               return playersList.Select(p => p.Position).Any(p => p.Id == positionId);
            }

            return false;

        }
       
    }
}