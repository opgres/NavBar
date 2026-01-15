using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.ReviewController.Models;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controllers.ReviewController
{
    [ApiController]
    [Route("review")]
    public class ReviewController : ControllerBase
    {
        public readonly ApplicationContext db;

        public ReviewController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ReviewRequestCreate reviewRequestCreate)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == reviewRequestCreate.UserId);
            if (user != null)
            {
                user.Reviews.Add(new Review
                {
                    UserId = reviewRequestCreate.UserId,
                    CocktailId = reviewRequestCreate.CocktailId,
                    Comment = reviewRequestCreate.Comment,
                    Score = reviewRequestCreate.Score,
                });


            }

            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд оценку коктейля");
            return Ok("Сохранили в бд оценку коктейля");
        }
    }
}
