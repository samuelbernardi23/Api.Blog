using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopCon.Api.Models;

namespace TopCon.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (model.Password != model.ConfirmPassword) return BadRequest();

            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                var roles = await userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault() ?? "";

                Response.Cookies.Append("UserId", user.Id);
                Response.Cookies.Append("Role", userRole);

                return Ok("Usuário registrado com sucesso.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    var user = await userManager.FindByEmailAsync(model.Email);

                    var roles = await userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault() ?? "";

                    Response.Cookies.Append("UserId", user.Id);
                    Response.Cookies.Append("Role", userRole);

                    return Ok(new { Message = $"Bem-vindo {user.UserName}" });
                }
                else
                {
                    return Unauthorized(new { message = "Usuário ou senha incorretos." });
                }
            }

            return BadRequest("Dados de login inválidos.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok(new { message = "Logout realizado com sucesso!" });
        }
    }
}
