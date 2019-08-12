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
        public Guid DraftId {get; set; }
        public TeamShallowModel Team {get; set;}

        public PlayerModel Player {get; set;}

    }
}
