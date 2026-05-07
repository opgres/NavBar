using NavBar.Models;

namespace NavBar.Controllers.CocktailController.Models
{
    public class CocktailResponseGetAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public float CostPrice { get; set; }
        public int CookingMethodId { get; set; }
        public CookingMethod CookingMethod { get; set; }

        public List<CocktailResponseGetAllIngredientInComposition> CocktailRequestGetAllIngredients { get; set; } = new();
        public List<CocktailResponseGetAllReview> CocktailRequestGetAllReviews { get; set; } = new();
        public List<CocktailResponseGetAllTag> CocktailRequestGetAllTags { get; set; } = new();

    }
    public class CocktailResponseGetAllTag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

    }
    public class CocktailResponseGetAllIngredientInComposition
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public float V { get; set; }

    }
    public class CocktailResponseGetAllReview
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool? IsFavorite { get; set; }

    }
}
