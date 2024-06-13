using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Please enter the customer's full name.")]
        [MaxLength(30, ErrorMessage = "The customer's full name cannot exceed 30 characters.")]
        public string CustomerFullName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the telephone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The telephone number must have 10 digits.")]
        public string Telephone { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the email address.")]
        [EmailAddress(ErrorMessage = "The email address is not in the correct format.")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the customer's birthday.")]
        [DataType(DataType.Date)]
        public DateOnly? CustomerBirthday { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm the password.")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
