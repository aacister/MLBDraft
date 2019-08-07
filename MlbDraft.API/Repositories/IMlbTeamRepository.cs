using MLBDraft.API.Entities;
using System;
using System.Collections.Generic;

namespace MLBDraft.API.Repositories
{
    public interface IMlbTeamRepository
    {

        MlbTeam GetMlbTeam(string teamAbbrev);

        
    }
}