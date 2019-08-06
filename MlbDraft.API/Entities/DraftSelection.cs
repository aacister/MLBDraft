using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class DraftSelection
    {
        [Key]       
        public Guid Id { get; set; }

        [ForeignKey("DraftId")]
        public Draft Draft {get; set;}

        public Guid DraftId {get; set;}

        [ForeignKey("TeamId")]
        public Team Team {get; set;}

        public Guid TeamId {get; set;}

        [ForeignKey("PlayerId")]
        public Player Player {get; set;}

        public Guid? PlayerId {get; set;}

        [Required]
        public string Round { get; set; }

        [Required]
        public string SelectionNo {get; set;}

    }
}
