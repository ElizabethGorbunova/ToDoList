using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApp.ModelsDto;
using ToDoWebApp.Services;

namespace ToDoWebApp.Controllers
{
    [Route("api/account")]
    [ApiController]
   
    public class AccountController: ControllerBase
    {
        public IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpPost]
        public ActionResult RegisterUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            accountService.RegisterUser(user);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto Logindto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var token = accountService.GenerateJwt(Logindto);
            return Ok(token);
        }
    }
}
