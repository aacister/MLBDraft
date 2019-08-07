using System;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class TeamUpdateModel : TeamAbstractModel
    {
         [Required]
        public override string Name { get; set; }
       
        [Required]
        public override string Owner { get; set; }

        [Required]
        public Guid? CatcherId {get; set;}
        [Required]
        public Guid? FirstBaseId {get; set;}
        [Required]
        public Guid? SecondBaseId {get; set;}

        [Required]
        public Guid? ThirdBaseId {get; set;}
        [Required]
        public Guid? ShortStopId {get; set;}
        [Required]
        public Guid? Outfield1Id {get; set;}

        [Required]
        public Guid? Outfield2Id {get; set;} 
        [Required]
         public Guid? Outfield3Id {get; set;} 

        
        public Guid? StartingPitcherId {get; set;} 

    }
}