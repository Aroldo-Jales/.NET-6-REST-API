using Microsoft.EntityFrameworkCore;
using Prova1.Domain.Entities.Authentication;

namespace Prova1.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        /* - COMMANDS
                - MIGRATIONS
                    dotnet ef migrations add InitialMigration --project .\Prova1.Infrastructure\ -o Database/SQLite/AppMigrations

                - UPDATE DATABASE (AFTER EACH MIGRATION)
                    dotnet ef database update --project .\Prova1.Infrastructure\
        */
        public DbSet<User>? Users { get; set; }
        public DbSet<UserValidationCode>? UserValidationCodes { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // FOR APPLICATION RUNNING USE AppDirectories.getDatabasePath
            //
            //optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", AppDirectories.getDatabasePath));

            // FOR MIGRATIONS AND DATABASE UPDATES USE ABSOLUTE PATH
            //
            optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", @"C:\Users\Aroldo Jales\Documents\Code\Vscode\IFPI\PROVA1\Prova1\Prova1.Infrastructure\Database\SQLite\AppDatabase.db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // UserValidationCode
            modelBuilder.Entity<UserValidationCode>().Property(u => u.Id).ValueGeneratedOnAdd();

            // RefreshTokens
            modelBuilder.Entity<RefreshToken>().Property(r => r.Id).ValueGeneratedOnAdd();
        }
    }
}