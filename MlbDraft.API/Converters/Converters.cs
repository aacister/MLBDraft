using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Entities;

namespace MLBDraft.API.Converters
{
    public class Converter : Profile{
        public Converter(){
            CreateMap<Player, PlayerModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"));

            CreateMap<PlayerCreateModel, Player>();

            CreateMap<Team, TeamModel>();
            CreateMap<TeamCreateModel, Team>();

            CreateMap<League, LeagueModel>();
            CreateMap<LeagueCreateModel, League>();

            CreateMap<User, UserModel>();
            CreateMap<User, CredentialModel>();

        }
    }
}