using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class CreateFDTypeDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [RegularExpression("^[a-zA-Z0-9\\s-]+$", ErrorMessage = "Name can only contain letters, numbers, spaces and hyphens")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Interest rate is required")]
        [Range(0.1, 100.0, ErrorMessage = "Interest rate must be between 0.1 and 100")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Minimum amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Minimum amount must be between 1,000 and 10,000,000")]
        public decimal MinAmount { get; set; }

        [Required(ErrorMessage = "Maximum amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Maximum amount must be between 1,000 and 10,000,000")]
        public decimal MaxAmount { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [RegularExpression("^(12|36)$", ErrorMessage = "Duration must be 12 (1 year) or 36 (3 years) months")]
        public int Duration { get; set; }
    }
} 