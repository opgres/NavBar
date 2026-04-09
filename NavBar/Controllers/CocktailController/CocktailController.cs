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
        public async Task<List<CocktailRequestGetAll>> ReadAll()
        {
            var cocktails = await db.Cocktails
                .Include(x => x.Compositions).ThenInclude(x => x.Ingredient)
                .Include(x => x.Reviews).ThenInclude(x => x.User)
                .Include(x => x.Tags)
                .ToListAsync();

            var allCocktails = new List<CocktailRequestGetAll>();
            foreach (var cocktail in cocktails)
            {
                var ingredients = new List<CocktailRequestGetAllIngredient>();
                foreach (var ingredient in cocktail.Compositions)
                {
                    ingredients.Add(new CocktailRequestGetAllIngredient
                    {
                        IngredientId = ingredient.IngredientId,
                        V = ingredient.V,
                        Name = ingredient.Ingredient.Name,
                    });
                }
                var reviews = new List<CocktailRequestGetAllReview>();
                foreach (var review in cocktail.Reviews)
                {
                    reviews.Add(new CocktailRequestGetAllReview
                    {
                        UserId = review.UserId,
                        Score = review.Score,
                        Comment = review.Comment,
                        Name = review.User.Name,
                    });
                }
                var tags = new List<CocktailRequestGetAllTag>();
                foreach (var tag in cocktail.Tags)
                {
                    tags.Add(new CocktailRequestGetAllTag
                    {
                        Name = tag.Name,
                    });
                }


                allCocktails.Add(new CocktailRequestGetAll
                {
                    Id = cocktail.Id,
                    Name = cocktail.Name,
                    Description = cocktail.Description,
                    Image = cocktail.Image,
                    CookingMethodId = cocktail.CookingMethodId,
                    CookingMethod = cocktail.CookingMethod,
                    CocktailRequestGetAllIngredients = ingredients,
                    CocktailRequestGetAllReviews = reviews,
                    CocktailRequestGetAllTags = tags,
                });
            }

            Console.WriteLine("Все коктейли:");
            return allCocktails;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(CocktailRequest cocktailRequest)
        {
            await db.Cocktails.Where(x => x.Id == cocktailRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили коктейль");
            return Ok("Удалили коктейль");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CocktailRequest cocktailRequest)
        {
            await db.Cocktails
                    .Where(x => x.Id == cocktailRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, cocktailRequest.Name));
            Console.WriteLine("Обновили коктейль");
            return Ok("Обновили коктейль");
        }

        [HttpGet("readAllFilter")]
        public async Task<List<Cocktail>> ReadAllFilter([FromQuery] Availability availability, [FromQuery] int[] ingredientIds,
            [FromQuery] int[] tagIds, [FromQuery] int[] scores, [FromQuery] int[] cookingMethodIds, [FromQuery] bool isFavorite)
        {
            var query = db.Cocktails.AsQueryable();
            query = query.Include(x => x.Tags).Include(x => x.CookingMethod)
            .Include(x => x.Reviews)
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
            var cocktails = await query.ToListAsync();

            return cocktails;
        }
    }
}
