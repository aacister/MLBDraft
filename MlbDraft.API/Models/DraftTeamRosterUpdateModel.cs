using System;
using System.ComponentModel.DataAnnotations;
using MLBDraft.API.Validators;

namespace MLBDraft.API.Models
{
    public class DraftTeamRosterUpdateModel : DraftTeamRosterAbstractModel
    {
       
        [Required]
        public override Guid DraftId { get; set; }

        [Required]
        public override Guid TeamId {get; set;}

        [Required]
        [TeamPlayerPosValidation("C")]
        public Guid? CatcherId {get; set;}

        [Required]
         [TeamPlayerPosValidation("1B")]
        public Guid? FirstBaseId {get; set;}

        [Required]
         [TeamPlayerPosValidation("2B")]
        public Guid? SecondBaseId {get; set;}

        [Required]
         [TeamPlayerPosValidation("3B")]
        public Guid? ThirdBaseId {get; set;}

        [Required]
         [TeamPlayerPosValidation("SS")]
        public Guid? ShortStopId {get; set;}

        [Required]
         [TeamPlayerPosValidation("OF1")]
        public Guid? Outfield1Id {get; set;}

        [Required]
         [TeamPlayerPosValidation("OF2")]
        public Guid? Outfield2Id {get; set;} 

        [Required]
         [TeamPlayerPosValidation("OF3")]
         public Guid? Outfield3Id {get; set;} 

         [Required]
         [TeamPlayerPosValidation("SP")]
        public Guid? StartingPitcherId {get; set;} 

    }
}