using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class LeagueModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int MinTeams { get; set; }

        public int MaxTeams { get; set; }

        public IList<TeamModel> Teams { get; set; }

        public LeagueModel(){
            this.Teams = new List<TeamModel>();
        }
            
    }
}
