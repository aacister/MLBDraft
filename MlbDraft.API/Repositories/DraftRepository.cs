using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class DraftRepository : IDraftRepository
    {
        private MLBDraftContext _context;
        
        public DraftRepository(MLBDraftContext context){
            _context = context;
        }

        public bool DraftExists(Guid draftId)
        {
            return _context.Drafts.Any(a => a.Id == draftId);
        }

        public bool DraftExistsForLeague(Guid leagueId, Guid Id)
        {
            return _context.Drafts.Any(a => a.League.Id == leagueId && a.Id == Id);
        }

        public Draft GetDraft(Guid draftId)
        {
            return _context.Drafts
            .Where(d => d.Id == draftId)
             .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Team)
             .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Player)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Team)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Catcher)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.FirstBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.SecondBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ShortStop)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ThirdBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield1)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield2)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield3)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.StartingPitcher)
            .FirstOrDefault();
        }

        public Draft GetDraftForLeague(Guid leagueId, Guid draftId)
        {
            return _context.Drafts
            .Where(d => d.Id == draftId && d.League.Id == leagueId)
             .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Team)
             .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Player)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Team)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Catcher)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.FirstBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.SecondBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ShortStop)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ThirdBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield1)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield2)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield3)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.StartingPitcher)
            .Include(d => d.League)
            .FirstOrDefault();
        }

        public IEnumerable<Draft> GetDrafts()
        {
            return _context.Drafts
                .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Team)
                .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Player) 
              .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Team)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Catcher)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.FirstBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.SecondBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ShortStop)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ThirdBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield1)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield2)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield3)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.StartingPitcher)
                .Include(d => d.League)
                .ToList();
        }

         public IEnumerable<Draft> GetDraftsForLeague(Guid leagueId)
        {
            return _context.Drafts
                .Where(d => d.League.Id == leagueId)
                .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Team)
                .Include(draft => draft.DraftSelections)
                    .ThenInclude(s => s.Player)
              .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Team)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Catcher)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.FirstBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.SecondBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ShortStop)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.ThirdBase)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield1)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield2)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.Outfield3)
            .Include(draft => draft.TeamRosters)
                .ThenInclude(t => t.StartingPitcher)
                .Include(d => d.League)
                .ToList();
        }



         public void AddDraft(Draft draft)
        {
            draft.Id = Guid.NewGuid();
            _context.Drafts.Add(draft);

             if (draft.DraftSelections.Any())
            {
                foreach (var selection in draft.DraftSelections)
                {
                    selection.Id = Guid.NewGuid();
                }
            }
        }

        public void AddDraftForLeague(Guid leagueId, Draft draft)
        {
            var league = _context.Leagues
            .Where(l => l.Id == leagueId)
            .FirstOrDefault();

            if(league != null)
            {
                draft.Id = Guid.NewGuid();
                draft.LeagueId = leagueId;
                league.Drafts.Add(draft);

                if (draft.DraftSelections.Any())
                {
                    foreach (var selection in draft.DraftSelections)
                    {
                        selection.Id = Guid.NewGuid();
                    }
                }
            }
        }

        public void UpdateDraftForLeague(Guid leagueId, Draft draftToUpdate, Draft draft)
        {
           var du = _context.Drafts
            .Where(t => t.Id == draftToUpdate.Id &&  t.League.Id == leagueId)
            .FirstOrDefault();
            
            if(du != null)
            {
                du.StartDate = draft.StartDate;
                du.EndDate = draft.EndDate;
            }
        }

        public void DeleteDraft(Draft draft)
        {
            _context.Drafts.Remove(draft);
        }

        
       
    }
}