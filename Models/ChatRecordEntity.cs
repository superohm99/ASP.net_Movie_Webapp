namespace ASP_Project.Models
{
    public class ChatRecordEntity
    {
        public int  Id {get;set;}
        public bool Status {get;set;}
        public CreateChatEntity CreateChatEntity {get;set;}
        public AppUser AppUser {get;set;}
    }
}