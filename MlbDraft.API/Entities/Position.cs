using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class Position
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Abbreviation {get; set;}

    }
}
