using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Project.Models
{
    public class ReportEntity
    {
        public int Id {get;set;}
        public string  Title {get;set;} = null!;
        public string Description {get;set;} = null!;
        // [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Sendtime {get;set;}
        public bool Status{get;set;}
        
        public string AppUserId {get;set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get;set;}
    }
}