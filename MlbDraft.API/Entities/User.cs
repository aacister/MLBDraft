using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class User
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]     
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
