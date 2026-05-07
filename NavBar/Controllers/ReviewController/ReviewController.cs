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

        //[HttpPost("create")]
        //public async Task<IActionResult> Create(ReviewRequestCreate reviewRequestCreate)
        //{
        //    var user = await db.Users.FirstOrDefaultAsync(x => x.Id == reviewRequestCreate.UserId);
        //    if (user != null)
        //    {
        //        user.Reviews.Add(new Review
        //        {
        //            UserId = reviewRequestCreate.UserId,
        //            CocktailId = reviewRequestCreate.CocktailId,
        //            Comment = reviewRequestCreate.Comment,
        //            Score = reviewRequestCreate.Score,
        //            IsFavorite = reviewRequestCreate.IsFavorite
        //        });


        //    }

        //    await db.SaveChangesAsync();
        //    Console.WriteLine("Сохранили в бд оценку коктейля");
        //    return Ok("Сохранили в бд оценку коктейля");
        //}

        [HttpPost("changeFavorites")]
        public async Task<IActionResult> ChangeFavorites(ReviewRequest reviewRequest)
        {
            var review = await db.Reviews.FirstOrDefaultAsync(r => r.CocktailId == reviewRequest.CocktailId
                                                                 & r.UserId == reviewRequest.UserId);
            if (review == null)
            {
                review = new Review();
                review.UserId = reviewRequest.UserId;
                review.CocktailId = reviewRequest.CocktailId;
                await db.Reviews.AddAsync(review);
            }

            if (reviewRequest.IsFavorite != null)
            {
                review.IsFavorite = reviewRequest.IsFavorite;
            }


            await db.SaveChangesAsync();
            return Ok("Изменили избранность коктейля");
        }

        [HttpPost("changeScore")]
        public async Task<IActionResult> ChangeScore(ReviewRequest reviewRequest)
        {
            var review = await db.Reviews.FirstOrDefaultAsync(r => r.CocktailId == reviewRequest.CocktailId
                                                                 & r.UserId == reviewRequest.UserId);
            if (review == null)
            {
                review = new Review();
                review.UserId = reviewRequest.UserId;
                review.CocktailId = reviewRequest.CocktailId;
                await db.Reviews.AddAsync(review);
            }

            if (reviewRequest.Score != null)
            {
                review.Score = reviewRequest.Score;
            }


            await db.SaveChangesAsync();
            return Ok("Изменили оценку коктейля");
        }

        [HttpPost("changeComment")]
        public async Task<IActionResult> ChangeComment(ReviewRequest reviewRequest)
        {
            var review = await db.Reviews.FirstOrDefaultAsync(r => r.CocktailId == reviewRequest.CocktailId
                                                                 & r.UserId == reviewRequest.UserId);
            if (review == null)
            {
                review = new Review();
                review.UserId = reviewRequest.UserId;
                review.CocktailId = reviewRequest.CocktailId;
                await db.Reviews.AddAsync(review);
            }

            if (reviewRequest.Comment != null)
            {
                review.Comment = reviewRequest.Comment;
            }


            await db.SaveChangesAsync();
            return Ok("Изменили оценку коктейля");
        }
        [HttpDelete("deleteComment")]
        public async Task<IActionResult> DeleteComment(ReviewRequest reviewRequest)
        {
            var review = await db.Reviews.FirstOrDefaultAsync(r => r.CocktailId == reviewRequest.CocktailId
                                                                 & r.UserId == reviewRequest.UserId);
            if (review != null)
            {
                review.Comment = null;
            }

            await db.SaveChangesAsync();
            return Ok("Изменили оценку коктейля");
        }
    }
}
