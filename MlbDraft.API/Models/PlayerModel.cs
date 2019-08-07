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
        public MlbTeamModel MlbTeam {get; set;} = new MlbTeamModel();

        public PositionModel Postion {get; set;} = new PositionModel();
        public string ImagePath { get; set; }
        public IList<PlayerStatsModel> Statistics {get; set;} = new List<PlayerStatsModel>();

    }
}
