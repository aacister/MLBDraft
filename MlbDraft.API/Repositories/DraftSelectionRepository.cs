using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class DraftSelectionRepository : IDraftSelectionRepository
    {
        private MLBDraftContext _context;
        
        public DraftSelectionRepository(MLBDraftContext context){
            _context = context;
        }


        public bool LeagueDraftSelectionExists(Guid draftId, Guid teamId, int round )
        {
            return _context.DraftSelections
            .Include(s => s.Team)
            .Include(s => s.Player)
            .Include(s => s.Draft)
                .ThenInclude(s => s.League)
            .Any(d => d.DraftId == draftId 
            && d.TeamId== teamId 
            && d.Round == round
            );

        }
        public DraftSelection GetLeagueDraftSelection(Guid draftId, Guid teamId, int round){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId 
                && s.TeamId == teamId 
                && s.Round == round
               )
             .Include(s => s.Team)
             .Include(s => s.Player)
              .Include(s => s.Draft)
                .ThenInclude(s => s.League)
            .FirstOrDefault();
        }

        public IEnumerable<DraftSelection> GetLeagueDraftSelectionsForLeague(Guid draftId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
              .Include(s => s.Draft)
                .ThenInclude(s => s.League)
             .ToList<DraftSelection>();

        }

        public IEnumerable<DraftSelection> GetLeagueDraftSelectionsForTeam(Guid draftId, Guid teamId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId &&  s.TeamId == teamId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
              .Include(s => s.Draft)
                .ThenInclude(s => s.League)
             .ToList<DraftSelection>();

        }

         public void AddDraftSelectionToDraft(Guid leagueId, Guid draftId, DraftSelection draftSelection){

            var draft = _context.Drafts
            .Where(d => d.Id == draftId && d.League.Id == leagueId)
            .FirstOrDefault();

            if(draft != null)
            {
                draft.DraftSelections.Add(draftSelection);
            }

        }

        public void UpdateDraftSelectionToDraft(Guid draftId, DraftSelection draftSelection){


            var draftSelectionToEdit = _context.DraftSelections
                .Where(d => d.DraftId == draftId
                && d.TeamId == draftSelection.TeamId 
                && d.Round == draftSelection.Round)
                .FirstOrDefault();

            if(draftSelectionToEdit != null)
            {
                draftSelectionToEdit.PlayerId = draftSelection.PlayerId;
            }

        }

        
    }
}