using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class CreateFDApplicationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FdtypeId { get; set; }

        [Required]
        [Range(1000, 10000000, ErrorMessage = "Amount must be between 1,000 and 10,000,000")]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression("^(12|36)$", ErrorMessage = "Duration must be 12 (1 year) or 36 (3 years) months")]
        public int Duration { get; set; }

        [Required]
        [Range(0.1, 100.0, ErrorMessage = "InterestRate must be between 0.1 and 100")]
        public decimal InterestRate { get; set; }
    }
} 