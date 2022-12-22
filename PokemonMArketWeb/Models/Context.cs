using Microsoft.EntityFrameworkCore;

namespace PokemonsMarketWeb.Models
{
    public class Context : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Comment> Comments { get; set; }

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
            modelBuilder.Entity<User>().Property(a => a.wallet).HasDefaultValue(5000);
            modelBuilder.Entity<User>().Property(a => a.role).HasDefaultValue("user");
            modelBuilder.Entity<Pokemon>().Property(a => a.UserId).HasDefaultValue(-1);
            modelBuilder.Entity<Pokemon>().Property(a => a.price).HasDefaultValue(300);
            modelBuilder.Entity<Pokemon>().Property(a => a.sellStatue).HasDefaultValue("selling");

        }
    }


}
