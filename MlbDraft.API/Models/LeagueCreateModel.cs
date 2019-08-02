using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class LeagueCreateModel : LeagueAbstractModel
    {

        [Required]
        public override string Name { get; set; }

        public override int MinTeams { get; set; } = 2;
        
        [Required]
        public override int MaxTeams { get; set; }
            
    }
}
