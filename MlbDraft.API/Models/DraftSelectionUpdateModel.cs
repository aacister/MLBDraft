using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftSelectionUpdateModel : DraftSelectionAbstractModel
    {

        [Required]
        public override Guid DraftId {get; set;}

        [Required]
        public override Guid TeamId {get; set;}

        [Required]        
        public override int Round { get; set; }

        [Required]
        public Guid PlayerId {get; set;}

   
  
    }
}
