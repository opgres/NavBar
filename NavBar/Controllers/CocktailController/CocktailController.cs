using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.CocktailController.Models;
using NavBar.Controllers.CocktailRequestontroller.Models;
using NavBar.DB;
using NavBar.Models;


namespace NavBar.Controllers.CocktailController
{
    [ApiController]
    [Route("cocktail")]
    public class CocktailController : ControllerBase
    {
        public readonly ApplicationContext db;

        public CocktailController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> NormalCreate(CocktailRequestCreate cocktailRequestCreate)
        {
            var cocktail = new Cocktail
            {
                Name = cocktailRequestCreate.Name,
                Description = cocktailRequestCreate.Description,
                Image = cocktailRequestCreate.Image,
                CookingMethodId = cocktailRequestCreate.CookingMethodId,
                Compositions = new(),
                Tags = new(),
            };

            await db.Cocktails.AddAsync(cocktail);

            foreach (var part in cocktailRequestCreate.CocktailRequestCreateIngredients)
            {
                cocktail.Compositions.Add(new Composition
                {
                    IngredientId = part.IngredientId,
                    V = part.V,
                });
            }

            foreach (var tag in cocktailRequestCreate.CocktailRequestCreateTags)
            {
                if (tag.TagId == 0)
                {
                    var newTag = new Tag { Name = tag.Name };
                    await db.Tags.AddAsync(newTag);
                    cocktail.Tags.Add(newTag);
                }
                else
                {
                    var oldTag = await db.Tags.FirstOrDefaultAsync(x => x.Id == tag.TagId);
                    if (oldTag != null)
                        cocktail.Tags.Add(oldTag);
                }
            }
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд коктейль");
            return Ok("Сохранили в бд коктейль");
        }

        //[HttpPost("create")] poor
        //public async Task<IActionResult> Create(CocktailRequest cocktailRequest)
        //{
        //    var cocktail = new Cocktail
        //    {
        //        Name = cocktailRequest.Name,
        //        Description = cocktailRequest.Description,
        //        Image = cocktailRequest.Image,
        //        CookingMethodId = cocktailRequest.CookingMethodId
        //    };
        //    await db.Cocktails.AddAsync(cocktail);
        //    await db.SaveChangesAsync();
        //    Console.WriteLine("Сохранили в бд коктейль");
        //    return Ok("Сохранили в бд коктейль");
        //}

        [HttpGet("readAll")]
        public async Task<List<CocktailResponseGetAll>> ReadAll()
        {
            var cocktails = await db.Cocktails
                .Include(x => x.Compositions).ThenInclude(x => x.Ingredient)
                .Include(x => x.Reviews).ThenInclude(x => x.User)
                .Include(x => x.Tags)
                .Include(x => x.CookingMethod)
                .ToListAsync();

            var allCocktails = Convert.ConvertCocktailsToCocktailResponseGetAll(cocktails);

            Console.WriteLine("Все коктейли с фильтрами");
            return allCocktails;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int cocktailId)
        {
            await db.Cocktails.Where(x => x.Id == cocktailId).ExecuteDeleteAsync();
            Console.WriteLine("Удалили коктейль");
            return Ok("Удалили коктейль");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CocktailRequestUpdate cocktailRequestUpdate)
        {
            var cocktail = await db.Cocktails
                .Include(x => x.Compositions).ThenInclude(x => x.Ingredient)
                .Include(x => x.Reviews).ThenInclude(x => x.User)
                .Include(x => x.Tags)
                .Include(x => x.CookingMethod)
                .Where(c => c.Id == cocktailRequestUpdate.Id)
                .FirstOrDefaultAsync();
            if (cocktail != null)
            {
                if (cocktailRequestUpdate.Name != null)
                {
                    cocktail.Name = cocktailRequestUpdate.Name;
                }
                if (cocktailRequestUpdate.Description != null)
                {
                    cocktail.Description = cocktailRequestUpdate.Description;
                }
                if (cocktailRequestUpdate.Image != null)
                {
                    cocktail.Image = cocktailRequestUpdate.Image;
                }
                if (cocktailRequestUpdate.CookingMethodId != null)
                {
                    cocktail.CookingMethodId = (int)cocktailRequestUpdate.CookingMethodId;
                }
                if (cocktailRequestUpdate.CocktailRequestUpdateIngredients != null)
                {
                    cocktail.Compositions.Clear();
                    foreach (var ingredient in cocktailRequestUpdate.CocktailRequestUpdateIngredients)
                    {
                        cocktail.Compositions.Add(new Composition
                        {
                            IngredientId = ingredient.IngredientId,
                            V = ingredient.V,
                        });
                    }
                }
                if (cocktailRequestUpdate.CocktailRequestUpdateTags != null)
                {
                    cocktail.Tags.Clear();
                    foreach (var tag in cocktailRequestUpdate.CocktailRequestUpdateTags)
                    {
                        if (tag.TagId == 0)
                        {
                            var newTag = new Tag { Name = tag.Name };
                            await db.Tags.AddAsync(newTag);
                            cocktail.Tags.Add(newTag);
                        }
                        else
                        {
                            var oldTag = await db.Tags.FirstOrDefaultAsync(x => x.Id == tag.TagId);
                            if (oldTag != null)
                                cocktail.Tags.Add(oldTag);
                        }
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine("Обновили коктейль");
            return Ok("Обновили коктейль");
        }

        [HttpGet("readAllFilter")]
        public async Task<List<CocktailResponseGetAll>> ReadAllFilter([FromQuery] Availability availability, [FromQuery] int[] ingredientIds,
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
            var cocktails = await query.ToListAsync();

            var allCocktails = Convert.ConvertCocktailsToCocktailResponseGetAll(cocktails);

            Console.WriteLine("Все коктейли с фильтрами");
            return allCocktails;
        }

        [HttpGet("read")]
        public async Task<CocktailResponseGetAll> Read([FromQuery] int cocktailId)
        {
            var cocktails = await db.Cocktails.Include(x => x.Tags).Include(x => x.CookingMethod)
                .Include(x => x.Reviews).ThenInclude(x => x.User)
                .Include(x => x.Compositions).ThenInclude(x => x.Ingredient)
                .Where(x => x.Id == cocktailId)
                .ToListAsync();

            var cocktailAll = Convert.ConvertCocktailsToCocktailResponseGetAll(cocktails).FirstOrDefault();
            Console.WriteLine("Ингредиент");
            return cocktailAll;
        }


    }



    public static class Convert
    {
        public static List<CocktailResponseGetAll> ConvertCocktailsToCocktailResponseGetAll(List<Cocktail> cocktails)
        {
            var cocktailsRequestGetAll = new List<CocktailResponseGetAll>();
            foreach (var cocktail in cocktails)
            {
                float costPrice = 0;
                var ingredientsInComposition = new List<CocktailResponseGetAllIngredientInComposition>();
                foreach (var ingredientInComposition in cocktail.Compositions)
                {
                    ingredientsInComposition.Add(new CocktailResponseGetAllIngredientInComposition
                    {
                        IngredientId = ingredientInComposition.IngredientId,
                        V = ingredientInComposition.V,
                        Name = ingredientInComposition.Ingredient.Name,
                    });
                    costPrice = ingredientInComposition.V * ingredientInComposition.Ingredient.PricePerVolume;
                }

                var reviews = new List<CocktailResponseGetAllReview>();
                foreach (var review in cocktail.Reviews)
                {
                    reviews.Add(new CocktailResponseGetAllReview
                    {
                        UserId = review.UserId,
                        Score = review.Score,
                        Comment = review.Comment,
                        Name = review.User.Name,
                        IsFavorite = review.IsFavorite,
                    });
                }

                var tags = new List<CocktailResponseGetAllTag>();
                foreach (var tag in cocktail.Tags)
                {
                    tags.Add(new CocktailResponseGetAllTag
                    {
                        TagId = tag.Id,
                        Name = tag.Name,
                    });
                }

                cocktailsRequestGetAll.Add(new CocktailResponseGetAll
                {
                    Id = cocktail.Id,
                    Name = cocktail.Name,
                    Description = cocktail.Description,
                    Image = cocktail.Image,
                    CostPrice = costPrice,
                    CookingMethodId = cocktail.CookingMethodId,
                    CookingMethod = cocktail.CookingMethod,
                    CocktailRequestGetAllIngredients = ingredientsInComposition,
                    CocktailRequestGetAllReviews = reviews,
                    CocktailRequestGetAllTags = tags,
                });
            }

            return cocktailsRequestGetAll;
        }
    }
}
