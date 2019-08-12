using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftSelectionCreateModel : DraftSelectionAbstractModel
    {

        [Required]
        public Guid DraftId { get; set; }

        [Required]
        public Guid TeamId {get; set;}

        public Guid? PlayerId {get; set;}

        [Required]        
        public override int Round { get; set; }

   
  
    }
}
