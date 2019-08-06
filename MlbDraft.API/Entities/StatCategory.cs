using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class StatCategory
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Abbreviation {get; set; }

        public ICollection<PlayerStatCategory> PlayerStatCategory { get; set; }
            = new List<PlayerStatCategory>();

    }
}
