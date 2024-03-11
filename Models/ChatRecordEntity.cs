using System.ComponentModel.DataAnnotations.Schema;
namespace ASP_Project.Models
{
    public class ChatRecordEntity
    {
        public int  Id {get;set;}
        public bool Status {get;set;}
        public int? ChatId {get;set;}
         [ForeignKey("ChatId")]
        public ChatEntity ChatEntity {get;set;}
        public List<MessageRecordEntity> MessageRecordEntities {get; set;}

        public string AppUserId {get; set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get;set;}
    }
}