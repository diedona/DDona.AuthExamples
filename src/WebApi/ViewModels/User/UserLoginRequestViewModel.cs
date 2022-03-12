using Domain.DataTransferObjects.User;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.User
{
    public class UserLoginRequestViewModel
    {
        [Required]
        [MaxLength(160)]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public static class Mapper
        {
            public static UserLoginRequestDTO ToDTO(UserLoginRequestViewModel vm)
            {
                return new UserLoginRequestDTO()
                {
                    Username = vm.Username,
                    Password = vm.Password
                };
            }
        }
    }
}
