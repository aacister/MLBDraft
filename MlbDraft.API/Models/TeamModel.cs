using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class TeamModel: TeamAbstractModel
    {
       
        public Guid Id { get; set; }

         public override string Name { get; set; }

        public override string OwnerId { get; set; }

        public Guid? LeagueId {get; set; }

        public LeagueModel League {get; set;}

        public Guid? CatcherId {get; set;}
        public PlayerModel Catcher {get; set;}

        public Guid? FirstBaseId {get; set;}
        public PlayerModel FirstBase {get; set;}
        public Guid? SecondBaseId {get; set;}
        public PlayerModel SecondBase {get; set;}

        public Guid? ThirdBaseId {get; set;}
        public PlayerModel ThirdBase {get; set;}

        public Guid? ShortStopId {get; set;}
        public PlayerModel ShortStop {get; set;}

        public Guid? Outfield1Id {get; set;}
        public PlayerModel Outfield1 {get; set;}

        public Guid? Outfield2Id {get; set;} 
        public PlayerModel Outfield2 {get; set;}

         public Guid? Outfield3Id {get; set;} 
         public PlayerModel Outfield3 {get; set;}

        public Guid? StartingPitcherId {get; set;} 
        public PlayerModel StartingPitcher {get; set;}

    }
}