using APPwithIdentity.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace APPwithIdentity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Blog> Blogs { get; set; }
    }

}
