using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftTeamRosterCreateModel : DraftTeamRosterAbstractModel
    {

        [Required]
        public override Guid DraftId { get; set; }

        [Required]
        public override Guid  TeamId {get; set; }
           

   
  
    }
}
