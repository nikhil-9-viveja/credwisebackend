using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class CreateFDTypeDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.1, 100.0, ErrorMessage = "InterestRate must be between 0.1 and 100")]
        public decimal InterestRate { get; set; }

        [Required]
        [Range(1000, 10000000, ErrorMessage = "MinAmount must be between 1,000 and 10,000,000")]
        public decimal MinAmount { get; set; }

        [Required]
        [Range(1000, 10000000, ErrorMessage = "MaxAmount must be between 1,000 and 10,000,000")]
        public decimal MaxAmount { get; set; }

        [Required]
        [RegularExpression("^(12|36)$", ErrorMessage = "Duration must be 12 (1 year) or 36 (3 years) months")]
        public int Duration { get; set; }
    }
} 