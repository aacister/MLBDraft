using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MLBDraft.API.Entities
{
    public class Player
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]     
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ImagePath {get; set;}
        
        [ForeignKey("MlbTeamId")]
        public MlbTeam MlbTeam {get; set;}

        public Guid? MlbTeamId {get; set;}

        [ForeignKey("PositionId")]
        public Position Position { get; set; }

       public Guid? PositionId {get; set;}

        public IEnumerable<PlayerStatCategory> StatCategories { get; set; }
            = new List<PlayerStatCategory>();

        

    }
}
