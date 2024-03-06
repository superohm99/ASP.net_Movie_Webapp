namespace ASP_Project.Models
{
    public class CreateChatEntity
    {
        public int? Id {get;set;}
        
        public int ChatEntityId {get;set;}
        public ChatEntity ChatEntity {get;set;}
        public AppUser AppUsers {get;set;}
    }
}