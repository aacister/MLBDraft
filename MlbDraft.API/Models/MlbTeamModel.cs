using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class MlbTeamModel 
    {
     
        public Guid Id { get; set; }

        public string Description { get; set; }
        
        public string Abbreviation {get; set; }

        public string LogoPath {get; set;}

    }
}
