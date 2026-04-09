using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavBar.Controllers.UserController.Models;
using NavBar.DB;
using NavBar.Models;

namespace NavBar.Controllers.UserController
{

    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        public readonly ApplicationContext db;

        public UserController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserRequest userRequest)
        {
            var user = new User
            {
                Name = userRequest.Name,
                Surname = userRequest.Surname
            };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            Console.WriteLine("Сохранили в бд пользователя");
            return Ok("Сохранили в бд пользователя");
        }

        [HttpGet("readAll")]
        public async Task<List<User>> ReadAll()
        {
            var users = await db.Users.ToListAsync();
            Console.WriteLine("Все пользователи:");
            return users;
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(UserRequest userRequest)
        {
            await db.Users.Where(x => x.Id == userRequest.Id).ExecuteDeleteAsync();
            Console.WriteLine("Удалили пользователя");
            return Ok("Удалили пользователя");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserRequest userRequest)
        {
            await db.Users
                    .Where(x => x.Id == userRequest.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Name, userRequest.Name)
                    .SetProperty(x => x.Surname, userRequest.Surname));
            Console.WriteLine("Обновили пользователя");
            return Ok("Обновили пользователя");
        }

        [HttpPost("entry")]
        public async Task<IActionResult> Entry(UserRequest userRequest)
        {

            var user = await db.Users
                .Where(x => x.Name == userRequest.Name)
                .Where(x => x.Surname == userRequest.Surname)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return Ok("Пользователь найден");
            }
            else
            {
                return Forbid("Пользователь не найден");
            }

        }
    }
}
