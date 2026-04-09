namespace NavBar.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float V { get; set; }
        public float AvgBuyPrice { get; set; }
        public string? Strength { get; set; }

        public int? TypeDrinkId { get; set; }
        public TypeDrink? TypeDrink { get; set; }

        public List<Composition> Compositions { get; set; } = new();
    }
}
