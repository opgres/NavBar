using NavBar.Models;

namespace NavBar.Controllers.FilterController.Models
{
    public class FilterCocktailResponse
    {
        public List<int> Scores { get; set; }
        public List<CookingMethod> cookingMethods { get; set; }
        public List<FilterCocktailResponseIngredient> filterIngredientResponseIngredients { get; set; }
        public List<FilterCocktailResponseTag> filterCocktailResponseTags { get; set; }

    }
    public class FilterCocktailResponseIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FilterCocktailResponseTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
