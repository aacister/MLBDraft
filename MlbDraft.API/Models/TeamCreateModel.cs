using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class TeamCreateModel
    {
       
        [Required]
         public string Name { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

      

    }
}