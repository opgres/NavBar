using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.TagController.Models;
using NavBar.DB;
using NavBar.Models;
namespace NavBar.Controllers.TagController
{

    [ApiController]
    [Route("tag")]
    public class TagController : ControllerBase
    {
        public readonly ApplicationContext db;

        public TagController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserRequest tagRequest)
        {
            var tag = new Tag { Name = tagRequest.Name };
            await db.Tags.AddAsync(tag);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд");
            return Ok();
        }

        [HttpGet("readAll")]
        public async Task<List<Tag>> ReadAll()
        {
            var tags = await db.Tags.ToListAsync();
            Console.WriteLine("Все Тэги:");
            return tags;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(UserRequest tagRequest)
        {
            await db.Tags.Where(x => x.Id == tagRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили тэг");
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserRequest tagRequest)
        {
            await db.Tags
                    .Where(x => x.Id == tagRequest.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, tagRequest.Name));
            Console.WriteLine("Обновили тэг");
            return Ok();
        }
    }
}

