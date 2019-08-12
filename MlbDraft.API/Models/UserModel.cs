using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class UserModel
    {
        [Required]
        public string UserName {get; set;}
        [Required]
        public string Token { get; set; }

        public IList<TeamShallowModel> Teams { get; set; } = new List<TeamShallowModel>();

    }
}