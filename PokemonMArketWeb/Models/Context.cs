using Microsoft.EntityFrameworkCore;

namespace PokemonsMarketWeb.Models
{
    public class Context : DbContext
    {

        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database= PokeMarket;" +
                "integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(a => a.mail).IsUnique(true);
            modelBuilder.Entity<User>().HasIndex(a => a.phone).IsUnique(true);
            modelBuilder.Entity<User>().HasIndex(a => a.userName).IsUnique(true);
            modelBuilder.Entity<User>().Property(a => a.userName).HasDefaultValue(5000);
        }
    }


}
