using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class League
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MinTeams { get; set; }

        [Required]
        public int MaxTeams { get; set; }

         public ICollection<Team> Teams { get; set; }
            = new List<Team>();

    }
}
