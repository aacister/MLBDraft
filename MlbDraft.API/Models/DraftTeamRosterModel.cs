using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftTeamRosterModel : DraftTeamRosterAbstractModel
    {
      
        public Guid Id { get; set; }

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
