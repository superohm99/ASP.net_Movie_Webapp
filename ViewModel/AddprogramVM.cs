using System.ComponentModel.DataAnnotations;

namespace ASP_Project.ViewModel
{
    public class AddprogramVM
    {
        // [DisplayFormat(DataFormatString = "{dd:mm}", ApplyFormatInEditMode = true)]
        public DateTime Showtime {get; set;}
        public int? Titlemovie  {get; set;}
        public int? Cinema  {get; set;}
        public int? County {get; set;}
        public int? Canton {get;set;}
    }
}