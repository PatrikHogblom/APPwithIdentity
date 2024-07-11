using APPwithIdentity.Data;
using APPwithIdentity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APPwithIdentity.Service
{
    public class BlogService : IService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(b => b.ApplicationUser).ToListAsync();
        }
        public async Task AddAsync(Blog blog, ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);

            // Validate if the user is authenticated
            if (userId == null)
            {
                throw new Exception("User is not authenticated!");
            }

            // Assign the UserId to the Blog entity
            blog.UserId = userId;

            // Add the Blog entity to the context
            _context.Blogs.Add(blog);

            try
            {
                // Save changes to the database asynchronously
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                // Log the exception for debugging purposes
                Console.WriteLine($"DbUpdateException occurred: {ex.Message}");
                throw; // Rethrow the exception to halt execution and see the full stack trace
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error occurred while adding blog: {ex.Message}");
                throw; // Rethrow the exception to halt execution and see the full stack trace
            }
        }
        public async Task UpdateAsync(Blog blog, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                throw new Exception("User is not authenticated!");
            }

            var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id && b.UserId == currentUser.Id);
            if (existingBlog == null)
            {
                throw new Exception("Blog not found or access denied.");
            }

            // Update the properties of the existing blog with values from the passed-in blog object
            existingBlog.Header = blog.Header;
            existingBlog.Text = blog.Text;
            existingBlog.Image = blog.Image;

            _context.Blogs.Update(existingBlog);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                throw new Exception("User is not authenticated!");
            }

            var blogToDelete = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && b.UserId == currentUser.Id);
            if (blogToDelete == null)
            {
                throw new Exception("Blog not found or access denied.");
            }

            _context.Blogs.Remove(blogToDelete);
            await _context.SaveChangesAsync();
        }

        // New method to get a blog by ID
        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }
    }
}
