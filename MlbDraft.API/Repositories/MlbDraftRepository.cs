using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLBDraft.API.Repositories
{
    public class MlbDraftRepository : IMlbDraftRepository
    {
        private MLBDraftContext _context;
        
        public MlbDraftRepository(MLBDraftContext context){
            _context = context;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}