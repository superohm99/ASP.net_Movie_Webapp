namespace ASP_Project.Models
{
    public class MessageRecordEntity
    {
        public int? Id {get; set;}
        public DateTime? Sendtime {get; set;}
        public string? Messagetext {get;set;}
        public ChatRecordEntity ChatRecordEntity {get;set;}
    }
}