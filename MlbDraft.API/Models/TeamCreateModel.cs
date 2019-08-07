using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class TeamCreateModel : TeamAbstractModel
    {
        [Required]
        public override string Name { get; set; }
       
       [Required]
        public override string Owner { get; set; }
    }
}