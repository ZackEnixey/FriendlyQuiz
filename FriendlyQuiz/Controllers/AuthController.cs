using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FriendlyQuiz.DTOs;
using FriendlyQuiz.Entities;
using FriendlyQuiz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FriendlyQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepo db;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public AuthController(IAuthRepo _db, IConfiguration _config,IMapper _mapper)
        {
            db = _db;
            config = _config;
            mapper = _mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {

            if (await db.UserExist(user.UserName.ToLower()))
            {
                return BadRequest("Username already exists");
            }
            var newUser = mapper.Map<User>(user);
            var createdUser = await db.Register(newUser, user.Password);
            return StatusCode(201);
        }
       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {
            var logedUser = await db.Login(user.Username.ToLower(), user.Password);
            if (logedUser == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logedUser.Id.ToString()),
                new Claim(ClaimTypes.Name,logedUser.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });


        }

        

    }
}
