namespace ASP_Project.Models
{
    public class ReportEntity
    {
        public int Id {get;set;}
        public string Description {get;set;}
        public DateTime Sendtime {get;set;}
        public bool Status{get;set;}
        public AppUser AppUser {get;set;}
    }
}