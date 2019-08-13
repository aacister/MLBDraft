using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftSelectionModel : DraftSelectionAbstractModel
    {
        public Guid Id { get; set; }
        public Guid? TeamId {get; set;}
        public Guid? PlayerId {get; set;}

    }
}
