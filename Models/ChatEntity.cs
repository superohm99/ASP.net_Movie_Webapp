using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP_Project.Data;

namespace ASP_Project.Models
{
    public class ChatEntity
    {
        [Key]
        public int? Id {get;set;}
        public DateTime? startAt {get;set;}
        public DateTime? endAt {get;set;}
        public TimeSpan? duration {get;set;}
        public int maxNumber {get;set;}
        public int remainNumber {get;set;} = 1;
        public int ProgramMovieEntityId {get;set;}
        [ForeignKey("ProgramMovieEntityId")]
        public ProgramMovieEntity ProgramMovieEntity {get;set;}
        // public AppUser AppUser {get; set;}

    }
}