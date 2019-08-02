using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MLBDraft.API.Models
{
    public class LeagueAbstractModel
    {

        public virtual string Name { get; set; }

        public virtual int MinTeams { get; set; }

        public virtual int MaxTeams { get; set; }


            
    }
}
