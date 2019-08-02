using System;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class TeamUpdateModel : TeamAbstractModel
    {
         [Required]
        public override string Name { get; set; }
       
       [Required]
        public override string OwnerId { get; set; }

        public Guid? CatcherId {get; set;}

        public Guid? FirstBaseId {get; set;}
        public Guid? SecondBaseId {get; set;}

        public Guid? ThirdBaseId {get; set;}

        public Guid? ShortStopId {get; set;}

        public Guid? Outfield1Id {get; set;}


        public Guid? Outfield2Id {get; set;} 


         public Guid? Outfield3Id {get; set;} 

        
        public Guid? StartingPitcherId {get; set;} 

    }
}