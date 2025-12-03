namespace NavBar.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int CookingMethodId { get; set; }
        public CookingMethod CookingMethod { get; set; }

        public List<Composition> Compositions { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();



        public List<Tag> Tags { get; set; } = new();

    }
}
