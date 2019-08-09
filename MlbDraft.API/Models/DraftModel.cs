using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftModel : DraftAbstractModel
    {
        public Guid Id {get; set; }

        public IList<DraftSelectionModel> DraftSelections { get; set; } = new List<DraftSelectionModel>();

    }
}
