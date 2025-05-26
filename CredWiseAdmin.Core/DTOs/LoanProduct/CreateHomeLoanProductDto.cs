using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class CreateHomeLoanProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        [Url(ErrorMessage = "Invalid image URL format")]
        public string ImageUrl { get; set; }
        [Required]
        [Range(1000, 10000000, ErrorMessage = "MaxLoanAmount must be between 1,000 and 10,000,000")]
        public decimal MaxLoanAmount { get; set; }
        [Required]
        [RegularExpression("^HOME$", ErrorMessage = "LoanType must be HOME")]
        public string LoanType { get; set; } = "HOME";
        [Required]
        [Range(0.1, 100.0, ErrorMessage = "InterestRate must be between 0.1 and 100")]
        public decimal InterestRate { get; set; }
        [Required]
        [Range(1, 600, ErrorMessage = "TenureMonths must be between 1 and 600")]
        public int TenureMonths { get; set; }
        [Required]
        [Range(0, 1000000, ErrorMessage = "ProcessingFee must be between 0 and 1,000,000")]
        public decimal ProcessingFee { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "DownPaymentPercentage must be between 0 and 100")]
        public decimal DownPaymentPercentage { get; set; }
    }
} 