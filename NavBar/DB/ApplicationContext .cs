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

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Composition>().HasKey(x => new { x.CocktailId, x.IngredientId });
            modelBuilder.Entity<Review>().HasKey(x => new { x.CocktailId, x.UserId });
        }
    }
}
