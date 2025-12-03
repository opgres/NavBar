namespace NavBar.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Cocktail> Cocktails { get; set; } = new();
    }
}
