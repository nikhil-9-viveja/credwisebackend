using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class UpdateLoanProductDto
    {
        [Required(ErrorMessage = "Loan product ID is required")]
        public int LoanProductId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be 3-100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be 10-500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid image URL format")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Maximum loan amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Maximum loan amount must be between 1,000 and 10,000,000")]
        public decimal MaxLoanAmount { get; set; }

        [Required(ErrorMessage = "Loan type is required")]
        [RegularExpression("^(HOME|PERSONAL|GOLD)$", ErrorMessage = "Invalid loan type. Must be HOME, PERSONAL, or GOLD")]
        public string LoanType { get; set; }

        public bool IsActive { get; set; }
    }
} 