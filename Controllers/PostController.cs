using Microsoft.AspNetCore.Mvc;
using TopCon.Api.Data;
using TopCon.Api.Models;
using Microsoft.EntityFrameworkCore;
using TopCon.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

namespace Blog.TopCon.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.OrderByDescending(p => p.CreatedAt)
                .Include(p => p.CreatedBy)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    CreatedBy = new
                    {
                        p.CreatedBy.Id,
                        p.CreatedBy.UserName,
                        p.CreatedBy.Email
                    }
                })
                .ToListAsync();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));

                var post = new Posts
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, post);
            }
            return BadRequest("Invalid model state");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, [FromBody] UpdatePostModel model)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            post.Title = model.Title;
            post.Content = model.Content;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(post.CreatedBy.Id != userId)
            {
                return BadRequest(new {message = "Não é possível excluir postagens de outros usuários."});
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Post removido com sucesso"});
        }
    }
}
