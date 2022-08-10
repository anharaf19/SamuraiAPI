using Microsoft.EntityFrameworkCore;
using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data
{
    public class SamuraiDbContext : DbContext
    {
        public SamuraiDbContext()
        {


        }
        public SamuraiDbContext(DbContextOptions<SamuraiDbContext> options) : base(options)
        {


        }

        public DbSet<Samurai> Samurais { get; set; }

        public DbSet<Sword> Swords { get; set; }

        public DbSet<SwordType> SwordTypes { get; set; }

        public DbSet<Element> Elements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SwordElement> SwordElements { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SamuraiDB")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Sword>().HasMany(s => s.Elements)
            .WithMany(b => b.Swords)
            .UsingEntity<SwordElement>(bs => bs.HasOne<Element>().WithMany(),
            bs => bs.HasOne<Sword>().WithMany());

            
        }

    }
}
