using Microsoft.EntityFrameworkCore;
using NavBar.Models;

namespace NavBar.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cocktail> Cocktails => Set<Cocktail>();
        public DbSet<Composition> Compositions => Set<Composition>();
        public DbSet<CookingMethod> CookingMethods => Set<CookingMethod>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<TypeDrink> TypeDrinks => Set<TypeDrink>();
        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
        }
    }
}
