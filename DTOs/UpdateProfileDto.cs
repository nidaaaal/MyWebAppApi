using MyWebAppApi.Helper;
using MyWebAppApi.Helper.AttributeValidation;
using System.ComponentModel.DataAnnotations;

namespace MyWebAppApi.DTOs
{
    public class UpdateProfileDto
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]

        [MaxLength(50)]

        public string LastName { get; set; } = null!;

        [MaxLength(50)]
        public string? DisplayName { get; set; }

        [Required]
        [DateOfBirth]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [MinLength(10), MaxLength(500)]
        public string Address { get; set; } = null!;

        [MaxLength(50)]
        public string? City { get; set; } = null;

        [MaxLength(50)]
        public string? State { get; set; } = null;

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter a valid 6-digit ZIP code")]
        public int ZipCode { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string Phone { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string? Mobile { get; set; } = null;
    }
}
