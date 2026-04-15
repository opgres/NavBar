namespace NavBar.Controllers.CocktailRequestontroller.Models
{
    public class CocktailRequestUpdate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int? CookingMethodId { get; set; }
        public List<CocktailRequestUpdateIngredient>? CocktailRequestUpdateIngredients { get; set; }
        public List<CocktailRequestUpdateTag>? CocktailRequestUpdateTags { get; set; }
    }
    public class CocktailRequestUpdateIngredient
    {
        public int IngredientId { get; set; }
        public float V { get; set; }
    }
    public class CocktailRequestUpdateTag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
    }
}
