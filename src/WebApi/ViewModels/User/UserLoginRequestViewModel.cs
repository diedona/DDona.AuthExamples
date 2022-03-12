using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.User
{
    public class UserLoginRequestViewModel
    {
        [Required]
        [MaxLength(160)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
