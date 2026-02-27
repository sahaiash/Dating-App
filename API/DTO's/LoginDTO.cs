using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.DTO_s
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public required string Email {get; set;}

        [Required]
        public required string Password {get; set;}
    }
}
