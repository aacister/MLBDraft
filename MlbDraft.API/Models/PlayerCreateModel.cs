using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class PlayerCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position {get; set;}
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
