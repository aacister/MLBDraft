using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class Draft
    {
        [Key]       
        public Guid Id { get; set; }

        [Required]     
        public int SelectionNo { get; set; }

        [Required]
        public string Password { get; set; }

         [ForeignKey("TeamId")]
        public Team Team {get; set; }

        public Guid TeamId {get; set;}

         [ForeignKey("PlayerId")]
        public Player Player {get; set; }

        public Guid PlayerId {get; set;}

    }
}
