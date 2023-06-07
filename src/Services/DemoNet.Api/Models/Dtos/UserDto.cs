using System.ComponentModel.DataAnnotations;

namespace DemoNet.Api.Models.Dtos
{
    public class UserDto
    {
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Confirmation Password is required.")]
        //[Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        //public string ConfirmPassword { get; set; }
    }
}
