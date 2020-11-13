using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyQuiz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriendlyQuiz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Users : Controller
    {
        private readonly IFriendlyQuizRepo db;
        public Users(IFriendlyQuizRepo _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await db.GetUsers();
            return  Ok(users);
        }



    }
}
