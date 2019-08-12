using AutoMapper;
using MLBDraft.API.Models;
using MLBDraft.API.Entities;
using System.Linq;
using AutoMapper.QueryableExtensions;


namespace MLBDraft.API.Converters
{
    public class Converter : Profile{
        public Converter(){

             CreateMap<User, UserModel>();

             CreateMap<StatCategory, PlayerStatsModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Abbreviation));

            CreateMap<PlayerStatCategory, PlayerStatsModel>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.StatCategory.Abbreviation));
           

            CreateMap<PlayerStatsModel, PlayerStatCategory>();

            CreateMap<Position, PositionModel>();

            CreateMap<MlbTeam, MlbTeamModel>();

            CreateMap<Player, PlayerModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.MlbTeamAbbreviation, opt => opt.MapFrom(src =>
                    src.MlbTeam.Abbreviation))
            .ForMember(dest => dest.PositionAbbreviation, opt => opt.MapFrom(src =>
                    src.Position.Abbreviation))
            .ForMember(dest => dest.MlbTeam, opt => opt.MapFrom(src =>
                    src.MlbTeam))
            .ForMember(dest => dest.Postion, opt => opt.MapFrom(src =>
                    src.Position))
            .ForMember(dest => dest.Statistics, opt => opt.MapFrom(src => 
                    src.StatCategories))
            .ForMember(dest => dest.HomeRuns, opt => opt.MapFrom(src => 
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "BA" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.Runs, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "R" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.HomeRuns, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "HR" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.RunsBattedIn, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "RBI" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.StolenBases, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "SB" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.InningsPitched, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "IP" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.Wins, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "W" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.Strikeouts, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "K" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.EarnedRunAverage, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "ERA" ).FirstOrDefault())
                    .Value))
            .ForMember(dest => dest.WHIP, opt => opt.MapFrom(src =>
                    (src.StatCategories.Where(x => x.StatCategory.Abbreviation == "WHIP" ).FirstOrDefault())
                    .Value));

            
            CreateMap<PlayerCreateModel, Player>();

            CreateMap<Team, TeamShallowModel>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.Username))
                .ForMember(dest => dest.LeagueId, opt => opt.MapFrom(src => 
                (src.League.Id)))
                .ForMember(dest => dest.CatcherId, opt => opt.MapFrom(src => 
                (src.Catcher.Id)))
                .ForMember(dest => dest.FirstBaseId, opt => opt.MapFrom(src => 
                (src.FirstBase.Id)))
                .ForMember(dest => dest.SecondBaseId, opt => opt.MapFrom(src => 
                (src.SecondBase.Id)))
                .ForMember(dest => dest.ThirdBaseId, opt => opt.MapFrom(src => 
                (src.ThirdBase.Id)))
                .ForMember(dest => dest.ShortStopId, opt => opt.MapFrom(src => 
                (src.ShortStop.Id)))
                .ForMember(dest => dest.Outfield1Id, opt => opt.MapFrom(src => 
                (src.Outfield1.Id)))
                .ForMember(dest => dest.Outfield2Id, opt => opt.MapFrom(src => 
                (src.Outfield2.Id)))
                .ForMember(dest => dest.Outfield3Id, opt => opt.MapFrom(src => 
                (src.Outfield3.Id)))
                .ForMember(dest => dest.StartingPitcherId, opt => opt.MapFrom(src => 
                (src.StartingPitcher.Id)))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.Username));

            CreateMap<Team, TeamModel>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.Username))
                .ForMember(dest => dest.LeagueId, opt => opt.MapFrom(src => 
                (src.League.Id)))
                .ForMember(dest => dest.Catcher, opt => opt.MapFrom(src => 
                (src.Catcher)))
                .ForMember(dest => dest.FirstBase, opt => opt.MapFrom(src => 
                (src.FirstBase)))
                .ForMember(dest => dest.SecondBase, opt => opt.MapFrom(src => 
                (src.SecondBase)))
                .ForMember(dest => dest.ThirdBase, opt => opt.MapFrom(src => 
                (src.ThirdBase)))
                .ForMember(dest => dest.ShortStop, opt => opt.MapFrom(src => 
                (src.ShortStop)))
                .ForMember(dest => dest.Outfield1, opt => opt.MapFrom(src => 
                (src.Outfield1)))
                .ForMember(dest => dest.Outfield2, opt => opt.MapFrom(src => 
                (src.Outfield2)))
                .ForMember(dest => dest.Outfield3, opt => opt.MapFrom(src => 
                (src.Outfield3)))
                .ForMember(dest => dest.StartingPitcher, opt => opt.MapFrom(src => 
                (src.StartingPitcher)));

            CreateMap<TeamCreateModel, Team>()
                 .ForMember(dest => dest.Owner, opt => opt.Ignore());
              //   .ForMember(dest => dest.League, opt => opt.Ignore());
            CreateMap<TeamUpdateModel, Team>()
                 .ForMember(dest => dest.Owner, opt => opt.Ignore());
            //     .ForMember(dest => dest.League, opt => opt.Ignore());

            CreateMap<League, LeagueModel>();
            CreateMap<LeagueCreateModel, League>();

            CreateMap<Draft, DraftModel>();
            CreateMap<DraftModel, Draft>();

            CreateMap<DraftSelection, DraftSelectionModel>();
            CreateMap<DraftSelectionCreateModel, DraftSelection>();

           
            CreateMap<User, CredentialModel>();

            CreateMap<CredentialModel, User>();

        }
    }
}