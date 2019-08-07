using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MLBDraft.API.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private MLBDraftContext _context;
        
        public PositionRepository(MLBDraftContext context){
            _context = context;
        }


        public Position GetPosition(string positionAbbrev)
        {
            return _context.Positions
            .FirstOrDefault(t => t.Abbreviation.ToLowerInvariant() == positionAbbrev.ToLowerInvariant());
        }
       
    }
}