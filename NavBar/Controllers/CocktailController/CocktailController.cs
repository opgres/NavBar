using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create(CocktailRequest cocktailRequest)
        {
            var cocktail = new Cocktail
            {
                Name = cocktailRequest.Name,
                Description = cocktailRequest.Description,
                Image = cocktailRequest.Image,
                CookingMethodId = cocktailRequest.CookingMethodId
            };
            await db.Cocktails.AddAsync(cocktail);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд коктейль");
            return Ok("Сохранили в бд коктейль");
        }

        [HttpGet("readAll")]
        public async Task<List<Cocktail>> ReadAll()
        {
            var cocktails = await db.Cocktails.ToListAsync();
            Console.WriteLine("Все коктейли:");
            return cocktails;
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
    }
}
