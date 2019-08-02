using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class PlayerCreateModel : PlayerAbstractModel
    {
        [Required]
        public override string FirstName { get; set; }
        [Required]
        public override string LastName { get; set; }
        [Required]
        public override string Team {get; set;}

        //TODO: Add byte[] Image {get; set;} -- convert to Player entity with custom model binder
        [Required]
        public override string Position {get; set;}
       
    }
}
