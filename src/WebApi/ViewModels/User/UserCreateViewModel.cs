using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.User
{
    public class UserCreateViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
