using System.ComponentModel.DataAnnotations.Schema;
namespace ASP_Project.Models
{
    public class MovieEntity
    {
        public int? Id {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        public string? Rating {get; set;}
        public DateTime? Showtime {get; set;}

        // public ChatEntity ChatEntity {get;set;}
        public int CinemaId {get;set;}
        [ForeignKey("CinemaId")]
        public CinemaEntity CinemaEntity {get;set;}
    }
}