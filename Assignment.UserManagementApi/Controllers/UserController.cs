using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Assignment.Business.Services;
using Assignment.UserData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Assignment.UserManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // POST api/user/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var existUser = _userService.ExistUser(user.Name);
                if (existUser)
                {
                    return BadRequest(new { message = "Name is exist!" });
                }
                var result = await _userService.AddUser(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // PUT api/user/edit
        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                var existUser = _userService.ExistUser(user.Name);
                if (existUser)
                {
                    return BadRequest(new { message = "Name is exist!" });
                }
                var result = await _userService.UpdateUser(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        // POST api/user/authenticate
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(Login login)
        {
            var user = await _userService.Authenticate(login.Email, login.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        //GET api/user/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var currentUser = await _userService.GetUserById(id);
            return new ActionResult<User>(currentUser);
        }

        public class Login
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}