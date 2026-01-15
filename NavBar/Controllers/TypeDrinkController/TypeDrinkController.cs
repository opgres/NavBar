using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.TypeDrinkController.Models;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controllers.TypeDrinkController
{
    [ApiController]
    [Route("typeDrink")]
    public class TypeDrinkController : ControllerBase
    {
        public readonly ApplicationContext db;

        public TypeDrinkController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TypeDrinkRequest typeDrinkRequest)
        {
            var typeDrink = new TypeDrink { Name = typeDrinkRequest.Name };
            await db.TypeDrinks.AddAsync(typeDrink);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд тип напитка");
            return Ok("Сохранили в бд тип напитка");
        }

        [HttpGet("readAll")]
        public async Task<List<TypeDrink>> ReadAll()
        {
            var typeDrinks = await db.TypeDrinks.ToListAsync();
            Console.WriteLine("Все типы напитка:");
            return typeDrinks;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(TypeDrinkRequest typeDrinkRequest)
        {
            await db.TypeDrinks.Where(x => x.Id == typeDrinkRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили тип напитка");
            return Ok("Удалили тип напитка");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(TypeDrinkRequest typeDrinkRequest)
        {
            await db.TypeDrinks
                    .Where(x => x.Id == typeDrinkRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, typeDrinkRequest.Name));
            Console.WriteLine("Обновили тип напитка");
            return Ok("Обновили тип напитка");
        }
    }
}
