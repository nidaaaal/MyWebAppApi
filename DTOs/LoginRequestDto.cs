using System.ComponentModel.DataAnnotations;

namespace MyWebAppApi.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;
    }
}
