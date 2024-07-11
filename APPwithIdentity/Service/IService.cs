using APPwithIdentity.Models.Entities;
using System.Security.Claims;

namespace APPwithIdentity.Service
{
    public interface IService
    {
        Task<List<Blog>> GetAllAsync();
        Task AddAsync(Blog blog, ClaimsPrincipal user);
        Task UpdateAsync(Blog blog, ClaimsPrincipal user);
        Task DeleteAsync(int id, ClaimsPrincipal user);
        Task<Blog> GetByIdAsync(int id);

    }
}
