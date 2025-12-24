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
                Color = ingredientRequest.Color,
                Ro = ingredientRequest.Ro,
                Strength = ingredientRequest.Strength,
                V = ingredientRequest.V,
                TypeDrinkId = ingredientRequest.TypeDrinkId
            };
            await db.Ingredients.AddAsync(ingredient);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд");
            return Ok();
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
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(IngredientRequest ingredientRequest)
        {
            await db.Ingredients
                    .Where(x => x.Id == ingredientRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, ingredientRequest.Name));
            Console.WriteLine("Обновили ингредиент");
            return Ok();
        }
    }
}
