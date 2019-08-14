using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftModel 
    {
        public Guid Id {get; set; }

        public virtual Guid LeagueId {get; set;}

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public IList<DraftSelectionModel> DraftSelections { get; set; } = new List<DraftSelectionModel>();

        public IList<DraftTeamRosterModel> TeamRosters { get; set; } = new List<DraftTeamRosterModel>();

    }
}
