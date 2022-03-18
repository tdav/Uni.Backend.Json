using App.Database;
using App.Models;
using App.Services;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace App.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private ILogger<UsersController> logger;

        public UsersController(IUnitOfWork _db, ILogger<UsersController> _logger)
        {
            db = _db;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] viAuthenticateModel model)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (string.IsNullOrEmpty(model.Login) && string.IsNullOrEmpty(model.Password))
            {
                logger.LogInformation($"Login Empty User:{model.Login} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var user = await rpUser.AuthenticateAsync(model);

            if (user == null)
            {
                logger.LogInformation($"Login BadRequest User:{model.Login} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            else
            {
                logger.LogInformation($"Login Ok User:{model.Login} Ip:{remoteIpAddress}");
            }

            return Ok(user);
        }


        [HttpPost("register")]
        [SwaggerOperation("Register")]
        public async Task<IActionResult> UserRegisterAsync([FromBody] viUserRegister model)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (string.IsNullOrEmpty(model.Login) && string.IsNullOrEmpty(model.Password))
            {
                logger.LogInformation($"Login Empty User:{model.Login} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var user = await rpUser.CreateUserAsync(model);

            if (user == null)
            {
                logger.LogInformation($"Login BadRequest User:{model.Login} Passw:{model.Password} Ip:{remoteIpAddress}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }            

            return Ok(user);
        }


        [HttpGet("get_users")]
        [SwaggerOperation("GetUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var res = await rpUser.GetAllUsersAsync();
            return Ok(res);
        }


        [HttpGet("get_user_by_id/{id}")]
        [SwaggerOperation("GetUserById")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var rpUser = db.GetRepository<tbUser>(true) as UserService;
            var res = await rpUser.GetByIdAsync(id);
            return Ok(res);
        }


        [HttpPost("set_role")]
        [SwaggerOperation("SetRole")]
        public async Task<IActionResult> SetRoleAsync([FromBody] viSetProperty model)
        {
            return Ok();
        }

        [HttpPost("remove_role")]
        [SwaggerOperation("RemoveRole")]
        public async Task<IActionResult> RemoveRoleAsync([FromBody] viSetProperty model)
        {
            return Ok();
        }

        [HttpPost("set_user_status")]
        [SwaggerOperation("SetUserStatus")]
        public async Task<IActionResult> SetUserStatusAsync([FromBody] viSetProperty model)
        {
            return Ok();
        }
    }
}
