using System.ComponentModel.DataAnnotations;

namespace ASP_Project.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string? Name {get; set;}
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string? Password {get; set;}

        [Compare("Password", ErrorMessage = "Passwords dont Match")]
        public string? ConfirmPassword {get; set;}
        public string? Address {get; set;}
    }
}