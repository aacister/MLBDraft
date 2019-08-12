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


        public bool LeagueDraftSelectionExists(Guid leagueId, Guid draftId, Guid teamId, int round )
        {
            return _context.DraftSelections.Any(d => d.Id == draftId 
            && d.Draft.League.Id == leagueId
            && d.Team.Id == teamId 
            && d.Round == round
            );

        }
        public DraftSelection GetLeagueDraftSelection(Guid leagueId, Guid draftId, Guid teamId, int round){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId 
                && s.Draft.League.Id == leagueId
                && s.Team.Id == teamId 
                && s.Round == round
               )
             .Include(s => s.Team)
             .Include(s => s.Player)
            .FirstOrDefault();
        }

        public IEnumerable<DraftSelection> GetLeagueDraftSelectionsForLeague(Guid leagueId, Guid draftId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId && s.Draft.League.Id == leagueId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
             .ToList<DraftSelection>();

        }

        public IEnumerable<DraftSelection> GetLeagueDraftSelectionsForTeam(Guid leagueId, Guid draftId, Guid teamId){
            return _context.DraftSelections
            .Where(s => s.Draft.Id == draftId && s.Draft.League.Id == leagueId && s.Team.Id == teamId) 
             .Include(s => s.Team)
             .Include(s => s.Player)
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

        
    }
}