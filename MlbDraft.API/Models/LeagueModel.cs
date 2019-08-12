using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class LeagueModel: LeagueAbstractModel
    {
        public Guid Id { get; set; }

        public override string Name { get; set; }

        public override int MinTeams { get; set; }

        public override int MaxTeams { get; set; }

        public IList<TeamShallowModel> Teams { get; set; } = new List<TeamShallowModel>();

        public IList<DraftModel> Drafts {get; set; } = new List<DraftModel>();

            
    }
}
