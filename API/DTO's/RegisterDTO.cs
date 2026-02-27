using System.ComponentModel.DataAnnotations;

namespace API.DTO_s
{
    public class RegisterDTO
    {

        [Required]
        public  string DisplayName {get; set;}="";

        [EmailAddress]
        [Required]
        public  string Email {get; set;}="";

        [Required]
        [MinLength(4)]
        public  string Password {get; set;}="";





    }
}
