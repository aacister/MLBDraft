using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IMlbDraftRepository
    {
        bool Save();
    }
}