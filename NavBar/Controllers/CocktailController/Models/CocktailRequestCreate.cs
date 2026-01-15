namespace NavBar.Controllers.CocktailController.Models
{
    public class CocktailRequestCreate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int CookingMethodId { get; set; }
        public List<CocktailRequestCreateIngredient> CocktailRequestCreateIngredients { get; set; }
        public List<CocktailRequestCreateTag> CocktailRequestCreateTags { get; set; }
    }
    public class CocktailRequestCreateIngredient
    {
        public int IngredientId { get; set; }
        public float V { get; set; }
    }
    public class CocktailRequestCreateTag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
    }
}

