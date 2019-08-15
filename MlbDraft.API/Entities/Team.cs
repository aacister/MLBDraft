using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class Team
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("OwnerId")]
        public MlbDraftUser Owner { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("LeagueId")]
        public League League {get; set; }

        public Guid? LeagueId {get; set;}

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
