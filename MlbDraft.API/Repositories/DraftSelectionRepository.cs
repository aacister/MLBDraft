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


        public bool DraftSelectionExists(Guid draftId, Guid teamId, int round, int selectionNo )
        {
            return _context.DraftSelections.Any(d => d.Id == draftId 
            && d.Team.Id == teamId 
            && d.Round == round
            && d.SelectionNo == selectionNo);

        }
        public DraftSelection GetDraftSelection(Guid draftId, Guid teamId, int round, int selectionNo){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId 
                && s.Team.Id == teamId 
                && s.Round == round
                && s.SelectionNo == selectionNo)
             .Include(s => s.Team)
             .Include(s => s.Player)
            .FirstOrDefault();
        }

        public IEnumerable<DraftSelection> GetDraftSelections(Guid draftId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
             .ToList<DraftSelection>();

        }

        public IEnumerable<DraftSelection> GetDraftSelectionsForTeam(Guid draftId, Guid teamId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId && s.Team.Id == teamId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
             .ToList<DraftSelection>();

        }

        public void AddDraftSelectionToDraft(Guid draftId, DraftSelection draftSelection){

            var draft = _context.Drafts
            .Where(d => d.Id == draftId)
            .FirstOrDefault();

            if(draft != null)
            {
                draft.DraftSelections.Add(draftSelection);
            }

        }

        
    }
}