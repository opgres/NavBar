using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.IngredientController.Models;

using NavBar.DB;
using NavBar.Models;


namespace NavBar.Controllers.IngredientController
{
    [ApiController]
    [Route("indredient")]
    public class IndredientController : ControllerBase
    {
        public readonly ApplicationContext db;

        public IndredientController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(IngredientRequestCreate ingredientRequestCreate)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientRequestCreate.Name,
                PricePerVolume = ingredientRequestCreate.BuyPrice / ingredientRequestCreate.V,
                Strength = ingredientRequestCreate.Strength,
                V = ingredientRequestCreate.V,
                TypeDrinkId = ingredientRequestCreate.TypeDrinkId
            };
            await db.Ingredients.AddAsync(ingredient);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд ингредиент");
            return Ok("Сохранили в бд ингредиент");
        }

        [HttpGet("readAll")]
        public async Task<List<Ingredient>> ReadAll()
        {
            var ingredients = await db.Ingredients.ToListAsync();
            Console.WriteLine("Все ингредиенты:");
            return ingredients;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int ingredientId)
        {
            await db.Ingredients.Where(x => x.Id == ingredientId).ExecuteDeleteAsync();
            Console.WriteLine("Удалили ингредиент");
            return Ok("Удалили ингредиент");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(IngredientRequestUpdate ingredientRequestUpdate)
        {
            var ingredient = await db.Ingredients.FindAsync(ingredientRequestUpdate.Id);
            if (ingredient != null)
            {
                if (ingredientRequestUpdate.Name != null)
                {
                    ingredient.Name = ingredientRequestUpdate.Name;
                }
                if (ingredientRequestUpdate.V != null)
                {
                    ingredient.V = (float)ingredientRequestUpdate.V;
                }
                if (ingredientRequestUpdate.Strength != null)
                {
                    ingredient.Strength = ingredientRequestUpdate.Strength;
                }
                if (ingredientRequestUpdate.TypeDrinkId != null)
                {
                    ingredient.TypeDrinkId = ingredientRequestUpdate.TypeDrinkId;
                }
                if (ingredientRequestUpdate.PricePerVolume != null)
                {
                    ingredient.PricePerVolume = (float)ingredientRequestUpdate.PricePerVolume;
                }
                db.SaveChanges();
            }
            Console.WriteLine("Обновили ингредиент");
            return Ok("Обновили ингредиент");
        }

        [HttpPut("buyToExist")]
        public async Task<IActionResult> BuyToExist(IngredientRequestBuyToExist ingredientRequestBuyToExist)
        {
            await db.Ingredients
                    .Where(x => x.Id == ingredientRequestBuyToExist.Id)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.V, x => x.V + ingredientRequestBuyToExist.VAdd)
                                              .SetProperty(x => x.PricePerVolume, ingredientRequestBuyToExist.BuyPrice / ingredientRequestBuyToExist.VAdd));

            Console.WriteLine("Обновили цену за объем у ингредиента");
            return Ok("Обновили цену за объем у ингредиента");
        }

        [HttpGet("readAllFilter")]
        public async Task<List<Ingredient>> ReadAllFilter([FromQuery] Availability availability, [FromQuery] int?[] typeDrinkIds = default)
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

            query = query.Include(x => x.TypeDrink);
            if (typeDrinkIds != null)
            {
                query = query.Where(x => typeDrinkIds.Contains(x.TypeDrinkId));
            }

            var ingredients = await query.ToListAsync();

            Console.WriteLine("Фильтрованные ингредиенты");
            return ingredients;
        }

        [HttpGet("read")]
        public async Task<Ingredient> Read([FromQuery] int ingredientId)
        {
            var ingredient = await db.Ingredients
                .Include(x => x.TypeDrink)
                .FirstOrDefaultAsync();

            Console.WriteLine("Ингредиент");
            return ingredient;
        }
    }
}
