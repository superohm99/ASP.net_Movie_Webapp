using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.Models
{
    public class MessageRecordEntity
    {
        public int? Id {get; set;}
        public DateTime? Sendtime {get; set;}
        public string? Messagetext {get;set;}
        public int? ChatRecordEntityId {get;set;}

        [ForeignKey("ChatRecordEntityId")]
        public ChatRecordEntity? ChatRecordEntity {get;set;}
    }
}