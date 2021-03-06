using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public abstract class PlayerAbstractModel
    {
   
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public string Name {get; set;}

        public virtual string MlbTeamAbbreviation {get; set;}

        //TODO: Add byte[] Image {get; set;} -- convert to Player entity with custom model binder

        public virtual string PositionAbbreviation {get; set;}
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
