using System.ComponentModel.DataAnnotations.Schema;
namespace ASP_Project.Models
{
    public class MovieEntity
    {
        public int? Id {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        public int? Rating {get; set;} = 0;

        public string? Image {get; set;}  = "https://lh3.googleusercontent.com/5U3o9cOloz01BbVZgqd9EnX6YR49SkMv22EROs8h9JvR6YQ2UMHuaV9odebRk1Hxs80cumWB80njRihnqLbMwG7dwjwCke5xYmE=w260";
        // public DateTime? Showtime {get; set;}

    }
}