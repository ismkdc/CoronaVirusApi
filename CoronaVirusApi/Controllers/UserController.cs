using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaVirusApi.Attributes;
using CoronaVirusApi.Models.DTO;
using CoronaVirusApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoronaVirusApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [SkipAuth]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(model);

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [SkipAuth]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(model);

                if (result)
                    return CreatedAtAction(nameof(Register), null);
                else
                    return BadRequest("Something happend when creating a user!");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("me")]
        public CurrentUserDTO Me() => _userService.CurrentUser;
    }
}