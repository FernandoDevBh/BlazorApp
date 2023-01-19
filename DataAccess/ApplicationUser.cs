using Microsoft.AspNetCore.Identity;

namespace DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
