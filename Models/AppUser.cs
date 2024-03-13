
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Rating { get; set; } = 0;
        public string? Image { get; set; } = "https://www.w3schools.com/howto/img_avatar.png";
        public string? IG { get; set; }
        public string? Facebook { get; set; }
        public List<ReportEntity> ReportEntities { get; set; }
        public List<ChatRecordEntity> ChatRecordEntities { get; set; }
    }
}

