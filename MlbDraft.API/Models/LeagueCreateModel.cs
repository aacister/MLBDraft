using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class LeagueCreateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int MinTeams { get; set; } = 2;
        
        [Required]
        public int MaxTeams { get; set; }
            
    }
}
