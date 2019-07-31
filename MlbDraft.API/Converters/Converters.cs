using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Entities;

namespace MLBDraft.API.Converters
{
    public class Converter : Profile{
        public Converter(){
            CreateMap<Player, PlayerModel>();
            CreateMap<PlayerCreateModel, Player>();
            CreateMap<Team, TeamModel>();
            CreateMap<League, LeagueModel>();

        }
    }
}