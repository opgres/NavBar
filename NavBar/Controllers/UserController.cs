using Microsoft.AspNetCore.Mvc;

namespace NavBar.Controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        public UserController() { }

        [HttpPost("create")]
        public string Create([FromBody] string name)
        {
            Console.WriteLine(name);
            return "Ok";
        }


    }
}
