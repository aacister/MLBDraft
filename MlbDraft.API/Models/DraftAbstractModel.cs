using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class DraftAbstractModel
    {

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }


            
    }
}
