namespace NavBar.Models
{
    public class Composition
    {
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public float V { get; set; }
    }
}
