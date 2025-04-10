using System.ComponentModel.DataAnnotations;
using AralytiksTest2.Models;

namespace AralytiksTest2.DTO
{
    public class UserDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword should be identically same!")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birthdate is required")]
        public DateOnly Birthdate { get; set; }

        public UserSettings? Settings { get; set; } = null;
    }
}
