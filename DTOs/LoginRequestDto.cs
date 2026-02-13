using System.ComponentModel.DataAnnotations;

namespace MyWebAppApi.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(255)]
        [RegularExpression(@"(^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$)|(^\d{10}$)",
        ErrorMessage = "Enter a valid email or 10-digit phone number")]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;
    }
}
