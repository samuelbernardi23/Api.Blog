using Microsoft.AspNetCore.Mvc;
using TopCon.Api.Data;
using TopCon.Api.Models;
using Microsoft.EntityFrameworkCore;
using TopCon.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Blog.TopCon.Api.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet (Name = "Login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Login");
        }
    }
}
