using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.Models
{
    public class FavoriteEntity
    {
        public int? Id {get; set;}
        
        public string AppUserId {get; set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get;set;}

        public int? MovieId {get; set;}
         [ForeignKey("MovieId")]
        public MovieEntity MovieEntity {get; set;}
    }
}