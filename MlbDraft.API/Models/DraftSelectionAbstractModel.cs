using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftSelectionAbstractModel
    {
      
        public virtual Guid DraftId { get; set; }
        public virtual Guid  TeamId {get; set; }
        public virtual int Round { get; set; }
           
    }
}
