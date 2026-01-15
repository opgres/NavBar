using NavBar.Models;

namespace NavBar.Controllers.CocktailController.Models
{
    public class CocktailRequestGetAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int CookingMethodId { get; set; }
        public CookingMethod CookingMethod { get; set; }

        public List<CocktailRequestGetAllIngredient> CocktailRequestGetAllIngredients { get; set; } = new();
        public List<CocktailRequestGetAllReview> CocktailRequestGetAllReviews { get; set; } = new();
        public List<CocktailRequestGetAllTag> CocktailRequestGetAllTags { get; set; } = new();

    }
    public class CocktailRequestGetAllTag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

    }
    public class CocktailRequestGetAllIngredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public float V { get; set; }

    }
    public class CocktailRequestGetAllReview
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }

    }
}
