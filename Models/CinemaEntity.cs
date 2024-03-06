namespace ASP_Project.Models
{
    public class CinemaEntity
    {
        public int? Id {get; set;}
        public string? Enterprise {get; set;}

        public MovieEntity MovieEntity {get;set;}
        public List<PlaceEntity> PlaceEntities {get;set;}
    }
}