using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MLBDraft.API.Entities
{
    public class MlbDraftUser : IdentityUser
    {

        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; }
            = new List<Team>();

        

    }
}
