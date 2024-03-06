namespace ASP_Project.Models
{
    public class MovieEntity
    {
        public int? Id {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        public string? Rating {get; set;}
        public DateTime? Showtime {get; set;}

        public List<ChatEntity> ChatEntities {get;set;}
        public List<CinemaEntity> CinemaEntities {get;set;}
    }
}