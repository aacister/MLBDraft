using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class User
    {

        [Key]     
        public string Username { get; set; }

        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<Team> Teams { get; set; }
            = new List<Team>();

        

    }
}
