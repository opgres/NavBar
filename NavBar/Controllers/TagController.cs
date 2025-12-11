using Microsoft.AspNetCore.Mvc;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controlers
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
        public string Create([FromBody] string name)
        {

            Tag tag = new Tag { Name = name };

            db.Tags.Add(tag);
            db.SaveChanges();
            Console.WriteLine("Сохранили в бд");
            return "Ok";
        }

        [HttpGet("readAll")]
        public List<Tag> ReadAll()
        {
            var tags = db.Tags.ToList();
            Console.WriteLine("Все Тэги:");

            return tags;
        }
        //[HttpGet("readCocktails")]
        //public List<Cocktail> ReadCocktails([FromQuery] string name)
        //{
        //    var tags = db.Tags.ToList();
        //    Console.WriteLine("Все Тэги:");

        //    return tags;
        //}
    }
}

