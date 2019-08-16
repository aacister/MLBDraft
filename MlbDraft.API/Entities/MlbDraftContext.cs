using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MLBDraft.API.Entities
{
    public class MLBDraftContext : IdentityDbContext
    {

        public MLBDraftContext(DbContextOptions<MLBDraftContext> options)
            : base(options)
        {
        
            Database.Migrate();
         }
        public DbSet<MlbTeam> MlbTeams {get; set;}
        public DbSet<Position> Positions {get; set; }
        public DbSet<StatCategory> StatCategories {get; set;}
        public DbSet<MlbDraftUser> MlbDraftUsers { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PlayerStatCategory> PlayerStatCategories {get; set;}
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<DraftSelection> DraftSelections {get; set;}
        public DbSet<DraftTeamRoster> DraftTeamRosters {get; set; }


        

    }
}
