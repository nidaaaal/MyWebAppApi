using MyWebAppApi.Helper;
using System.ComponentModel.DataAnnotations;

namespace MyWebAppApi.DTOs
{
    public class RegisterRequestDto
    {
            [MaxLength(255)]
            public string UserName { get; set; } = null!;

            [Required]
            [MaxLength(50)]
            public string Password { get; set; } = null!;

            [Required]
            [MinLength(3),MaxLength(50)]
            public string FirstName { get; set; } = null!;

            [Required]

            [MaxLength(50)]

            public string? LastName { get; set; }

            [MaxLength(50)]
            public string? DisplayName { get; set; }

            [Required]
            [DateOfBirth]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public bool Gender { get; set; }

            [Required]
            [MaxLength(500)]
            public string Address { get; set; } = null!;
            
            [MaxLength(50)]
            public string? City { get; set; } = null;

            [MaxLength(50)]
            public string? State { get; set; } = null;

            [Required]
            [Range(100000,999999)]
            public int ZipCode { get; set; }

            [Required]
            [MaxLength(10)]
            public string Phone { get; set; }= null!;
            [MaxLength(10)]
            public string? Mobile { get; set; } = null;
    }
}
