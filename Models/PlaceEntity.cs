
namespace ASP_Project.Models
{
    public  class PlaceEntity
    {
        public int? Id {get; set;}
        public string? County {get; set;}
        public string? Canton {get; set;}

        public CinemaEntity CinemaEntity {get; set;}
    }
}