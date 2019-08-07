using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class MlbTeamRepository : IMlbTeamRepository
    {
        private MLBDraftContext _context;
        
        public MlbTeamRepository(MLBDraftContext context){
            _context = context;
        }


        public MlbTeam GetMlbTeam(string teamAbbrev)
        {
            return _context.MlbTeams
            .FirstOrDefault(t => t.Abbreviation.ToLowerInvariant() == teamAbbrev.ToLowerInvariant());
        }
       
    }
}