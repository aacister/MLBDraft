using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class TeamModel
    {
       
        public Guid Id { get; set; }

         public string Name { get; set; }

         public UserModel Owner { get; set; }

        public Guid OwnerId { get; set; }

        public LeagueModel League {get; set; }

        public PlayerModel Catcher {get; set;}

        public PlayerModel FirstBase {get; set;}
        public PlayerModel SecondBase {get; set;}

        public PlayerModel ThirdBase {get; set;}

        public PlayerModel ShortStop{get; set;}


        public PlayerModel Outfield1 {get; set;}


        public PlayerModel Outfield2 {get; set;}


        public PlayerModel Outfield3 {get; set;}

        
        public PlayerModel StartingPitcher {get; set;}


    }
}