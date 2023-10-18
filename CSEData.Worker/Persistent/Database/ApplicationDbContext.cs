using Microsoft.EntityFrameworkCore;
using Persistent.Entity;

namespace Persistent.Database
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {

        public readonly string _ConnectionString;
        public readonly string _migrationString;

        public ApplicationDbContext(string connectionString, string migrationString)
       {
            _ConnectionString = connectionString;
            _migrationString = migrationString;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString,

                    (x) => x.MigrationsAssembly(_migrationString)
                    );
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void ConfigureConventions
              (ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }


      public  DbSet<Company> companies { get; }
       public DbSet<Price> prices { get; }


    }
}
