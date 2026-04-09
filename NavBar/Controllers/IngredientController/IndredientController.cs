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
        public async Task<IActionResult> Create(IngredientRequest ingredientRequest)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientRequest.Name,
                AvgBuyPrice = ingredientRequest.AvgBuyPrice,
                Strength = ingredientRequest.Strength,
                V = ingredientRequest.V,
                TypeDrinkId = ingredientRequest.TypeDrinkId
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
        public async Task<IActionResult> Delete(IngredientRequest ingredientRequest)
        {
            await db.Ingredients.Where(x => x.Id == ingredientRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили ингредиент");
            return Ok("Удалили ингредиент");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(IngredientRequest ingredientRequest)
        {
            await db.Ingredients
                    .Where(x => x.Id == ingredientRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, ingredientRequest.Name));
            Console.WriteLine("Обновили ингредиент");
            return Ok("Обновили ингредиент");
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
