using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.Models
{
    public class RequestEntity
    {
        public int? Id {get; set;}
        public bool Status {get; set;}
        public string AppUserId {get;set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get;set;}
        public int? ChatRecordId {get;set;}
        [ForeignKey("ChatRecordId")]
        public ChatRecordEntity ChatRecordEntity {get;set;}
    }
}