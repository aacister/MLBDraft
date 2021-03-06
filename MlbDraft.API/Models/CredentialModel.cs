using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLBDraft.API.Models
{
    public class CredentialModel : CredentialAbstractModel
    {
        [Required]
        public override string UserName { get; set; }
        [Required]
        public override string Password { get; set; }
    }
}