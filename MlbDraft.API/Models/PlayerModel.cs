using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MLBDraft.API.Models
{
    public class PlayerModel : PlayerAbstractModel
    {
        [Required]
        public Guid Id { get; set; }
        public string ImagePath { get; set; }

    }
}
