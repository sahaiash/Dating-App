using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTO_s
{
    public class UserDTO
    {
        [Required]
       public required string Id { get; set; }

       [Required]
       public required string Email {get; set;}

       [Required]
       public required string DisplayName {get; set;}

       [Required]

       public  string? ImageUrl {get; set;}

       [Required]
       public required string Token {get; set;}


    }
}
