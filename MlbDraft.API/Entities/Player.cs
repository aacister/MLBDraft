using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class Player
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]     
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ImagePath {get; set;}
        
        [Required]
        public string Team {get; set;}

        [Required]
        public string Position { get; set; }

        public string BattingAverage {get; set; }

        public string HomeRuns {get; set;}
        public string Runs {get; set;}

        public string RunsBattedIn {get; set;}
        public string StolenBases {get; set;}
        public string InningsPitched {get; set;}
        public string Wins {get; set;}
        public string Strikeouts {get; set;}
        public string EarnedRunAverage {get; set;}
        public string WHIP {get; set;}

    }
}
