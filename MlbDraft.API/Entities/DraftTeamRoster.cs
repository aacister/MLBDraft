using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class DraftTeamRoster
    {
        [Key]       
        public Guid Id { get; set; }

        [ForeignKey("DraftId")]
        public Draft Draft {get; set;}

        public Guid DraftId {get; set;}

        [ForeignKey("TeamId")]
        public Team Team {get; set;}

        public Guid TeamId {get; set;}

        [ForeignKey("CatcherId")]
        public Player Catcher {get; set;}

        public Guid? CatcherId {get; set;}

        [ForeignKey("FirstBaseId")]
        public Player FirstBase {get; set;}

        public Guid? FirstBaseId {get; set;}

        [ForeignKey("SecondBaseId")]
        public Player SecondBase {get; set;}

        public Guid? SecondBaseId {get; set;}

        [ForeignKey("ThirdBaseId")]
        public Player ThirdBase {get; set;}

        public Guid? ThirdBaseId {get; set;}

        [ForeignKey("ShortStopId")]
        public Player ShortStop{get; set;}

        public Guid? ShortStopId {get; set;}

        [ForeignKey("Outfield1Id")]
        public Player Outfield1 {get; set;}

        public Guid? Outfield1Id {get; set;}

        [ForeignKey("Outfield2Id")]
        public Player Outfield2 {get; set;}

        public Guid? Outfield2Id {get; set;}

        [ForeignKey("Outfield3Id")]
        public Player Outfield3 {get; set;}

        public Guid? Outfield3Id {get; set;}
        
        [ForeignKey("StartingPitcherId")]
        public Player StartingPitcher {get; set;}

        public Guid? StartingPitcherId {get; set;}

 

    }
}