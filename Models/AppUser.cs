
using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name {get;set;}
        public string? Address {get;set;}
    }
}