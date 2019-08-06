using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLBDraft.API.Entities
{
    public class PlayerStatCategory
    {
        [Key]
        public Guid Id {get; set;}
        
        [ForeignKey("PlayerId")]
        public Player Player {get; set;}

        public Guid PlayerId {get; set;}

        [ForeignKey("StatCategoryId")]
        public StatCategory StatCategory {get; set;}

        public Guid StatCategoryId {get; set;}

        public string Value {get; set;}



    }
}
