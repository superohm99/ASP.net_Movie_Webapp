using System.ComponentModel.DataAnnotations.Schema;
namespace ASP_Project.Models
{
    public class MovieEntity
    {
        public int? Id {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        public int? Rating {get; set;} = 0;
        // public DateTime? Showtime {get; set;}

    }
}