using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MLBDraft.API.Entities
{
    public class MLBDraftContext : DbContext
    {

        public MLBDraftContext(DbContextOptions<MLBDraftContext> options)
            : base(options)
        {
            Database.Migrate();
         }
        
        public DbSet<User> Users { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Draft> Drafts { get; set; }


        

    }
}
