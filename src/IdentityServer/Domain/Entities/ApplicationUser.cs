using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Domain.Entities
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string ImageUrl { get;  set; }
        public string FullName { get;  set; }
    }
}