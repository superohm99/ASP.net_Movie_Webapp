using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Models
{
    public class ChatEntity
    {
        [Key]
        public int Id {get;set;}
        public DateTime startAt {get;set;}
        public DateTime endAt {get;set;}
        public int duration {get;set;}

        public MovieEntity MovieEntity {get;set;}
        // public AppUser AppUser {get; set;}
    }
}