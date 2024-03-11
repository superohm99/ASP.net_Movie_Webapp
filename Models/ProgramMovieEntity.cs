using System.ComponentModel.DataAnnotations.Schema;
namespace ASP_Project.Models
{
    public  class ProgramMovieEntity
    {
        public int? Id {get; set;}
        public DateTime Showtime {get; set;}
        public int? MovieId {get; set;}
         [ForeignKey("MovieId")]
        public MovieEntity MovieEntity {get; set;}
        public int? PlaceId {get; set;}
         [ForeignKey("PlaceId")]
        public PlaceEntity PlaceEntity {get;set;}
        public int? CinemaId {get;set;}
         [ForeignKey("CinemaId")]
        public CinemaEntity CinemaEntity {get;set;}
    }
}