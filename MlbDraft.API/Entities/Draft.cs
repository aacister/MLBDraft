using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class Draft
    {
        [Key]       
        public Guid Id { get; set; }

        [ForeignKey("LeagueId")]
        public League League {get; set; }

        public Guid LeagueId {get; set;}
        
        [Required]
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}

 

    }
}
