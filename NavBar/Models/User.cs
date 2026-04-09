namespace NavBar.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Review> Reviews { get; set; } = new();

        public List<Cocktail> CustomCocktails { get; set; } = new();
    }
}
