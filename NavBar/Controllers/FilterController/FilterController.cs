using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.FilterController.Models;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controllers.FilterController
{
    [ApiController]
    [Route("filter")]
    public class FilterController : ControllerBase
    {
        public readonly ApplicationContext db;

        public FilterController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet("ingredient")]
        public async Task<List<TypeDrink>> Ingredient([FromQuery] Availability availability)
        {
            var query = db.Ingredients.AsQueryable();
            switch (availability)
            {
                case Availability.Available:
                    query = query.Where(x => x.V > 0);
                    break;
                case Availability.Unavailable:
                    query = query.Where(x => x.V <= 0);
                    break;
                case Availability.All:
                    break;
                default:
                    query = query.Where(x => x.V > 0);
                    break;
            }

            var typeDrinks = await query.Include(x => x.TypeDrink)
                .Select(i => i.TypeDrink).Distinct().ToListAsync();


            Console.WriteLine("Фильтр ингредиентов");
            return typeDrinks;
        }


        [HttpGet("cocktail")]
        public async Task<FilterCocktailResponse> Cocktail([FromQuery] Availability availability, [FromQuery] int[] ingredientIds,
            [FromQuery] int[] tagIds, [FromQuery] int?[] scores, [FromQuery] int[] cookingMethodIds, [FromQuery] bool? isFavorite = default)
        {
            var query = db.Cocktails.AsQueryable();
            query = query.Include(x => x.Tags).Include(x => x.CookingMethod)
                .Include(x => x.Reviews).ThenInclude(x => x.User)
                .Include(x => x.Compositions).ThenInclude(x => x.Ingredient);
            switch (availability)
            {
                case Availability.Available:
                    query = query.Where(x => x.Compositions.Any(i => i.Ingredient.V > 0));
                    break;
                case Availability.Unavailable:
                    query = query.Where(x => x.Compositions.Any(i => i.Ingredient.V <= 0));
                    break;
                case Availability.All:
                    break;
                default:
                    query = query.Where(x => x.Compositions.Any(i => i.Ingredient.V > 0));
                    break;
            }

            if (ingredientIds.Length > 0)
            {
                query = query.Where(x => x.Compositions.Any(c => ingredientIds.Contains(c.IngredientId)));
            }

            if (tagIds.Length > 0)
            {
                query = query.Where(x => x.Tags.Any(t => tagIds.Contains(t.Id)));
            }

            if (scores.Length > 0)
            {
                query = query.Where(x => x.Reviews.Any(r => scores.Contains(r.Score)));
            }

            if (cookingMethodIds.Length > 0)
            {
                query = query.Where(x => cookingMethodIds.Contains(x.CookingMethodId));
            }

            if (isFavorite != null)
            {
                query = query.Where(x => x.Reviews.Any(r => r.IsFavorite == isFavorite));
            }

            var filters = new FilterCocktailResponse();

            filters.cookingMethods = await query.Select(c => c.CookingMethod).Distinct().ToListAsync();
            var tags = await query.Select(i => i.Tags).Distinct().FirstOrDefaultAsync();

            foreach (var tag in tags)
            {
                filters.filterCocktailResponseTags.Add(new FilterCocktailResponseTag
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }

            var reviews = await query.Select(c => c.Reviews).Distinct().FirstOrDefaultAsync();

            foreach (var review in reviews)
            {
                if (review != null)
                {
                    filters.Scores.Add((int)review.Score);
                }
            }



            var compositions = await query.Select(c => c.Compositions).FirstOrDefaultAsync();
            var ingredients = compositions.Select(c => c.Ingredient).Distinct().ToList();

            foreach (var ingredient in ingredients)
            {
                filters.filterIngredientResponseIngredients.Add(new FilterCocktailResponseIngredient
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name
                });
            }


            return filters;
        }

    }
}
