using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APPwithIdentity.Models.Entities;
using APPwithIdentity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APPwithIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogsController(BlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/blogs
        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving blogs: {ex.Message}");
            }
        }
        // POST: api/blogs
        [HttpPost]
        public async Task<ActionResult<Blog>> AddBlog(Blog blog)
        {
            try
            {
                // Get the current user's claims principal
                ClaimsPrincipal user = this.User;

                await _blogService.AddAsync(blog, user);

                return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding blog: {ex.Message}");
            }
        }

        // PUT: api/blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, Blog blog)
        {
            try
            {
                if (id != blog.Id)
                {
                    return BadRequest("Blog ID mismatch.");
                }

                // Get the current user's claims principal
                ClaimsPrincipal user = this.User;

                await _blogService.UpdateAsync(blog, user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating blog: {ex.Message}");
            }
        }

        // DELETE: api/blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                // Get the current user's claims principal
                ClaimsPrincipal user = this.User;

                await _blogService.DeleteAsync(id, user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting blog: {ex.Message}");
            }
        }
        // Helper method to get a blog by ID
        private async Task<ActionResult<Blog>> GetBlogById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound($"Blog with ID {id} not found.");
            }

            return blog;
        }


    }
}
