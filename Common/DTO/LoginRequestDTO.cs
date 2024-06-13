using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
