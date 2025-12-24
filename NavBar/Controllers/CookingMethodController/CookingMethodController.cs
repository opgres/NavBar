using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.CookingMethodController.Models;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controllers.CookingMethodController
{
    [ApiController]
    [Route("cookingMethod")]
    public class CookingMethodController : ControllerBase
    {
        public readonly ApplicationContext db;

        public CookingMethodController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CookingMethodRequest cookingMethodRequest)
        {
            var cookingMethod = new CookingMethod { Name = cookingMethodRequest.Name };
            await db.CookingMethods.AddAsync(cookingMethod);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд ");
            return Ok("Сохранили в бд ");
        }

        [HttpGet("readAll")]
        public async Task<List<CookingMethod>> ReadAll()
        {
            var cookingMethods = await db.CookingMethods.ToListAsync();
            Console.WriteLine("Все :");
            return cookingMethods;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(CookingMethodRequest cookingMethodRequest)
        {
            await db.CookingMethods.Where(x => x.Id == cookingMethodRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили ");
            return Ok("Удалили ");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CookingMethodRequest cookingMethodRequest)
        {
            await db.CookingMethods
                    .Where(x => x.Id == cookingMethodRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, cookingMethodRequest.Name));
            Console.WriteLine("Обновили ");
            return Ok("Обновили ");
        }
    }
}
