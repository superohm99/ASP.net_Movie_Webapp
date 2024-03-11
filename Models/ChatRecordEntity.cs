namespace ASP_Project.Models
{
    public class ChatRecordEntity
    {
        public int  Id {get;set;}
        public bool Status {get;set;}
        public ChatEntity ChatEntity {get;set;}
        public List<MessageRecordEntity> MessageRecordEntities {get; set;}
        public AppUser AppUser {get;set;}
    }
}