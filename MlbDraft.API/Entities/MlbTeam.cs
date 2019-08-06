using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class MlbTeam
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string Abbreviation {get; set; }

        public string LogoPath {get; set;}



    }
}
