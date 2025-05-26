using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class CreateLoanProductDto
    {
        // Base fields
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
        [RegularExpression("^(HOME|PERSONAL|GOLD)$", ErrorMessage = "LoanType must be HOME, PERSONAL, or GOLD")]
        public string LoanType { get; set; } // HOME, PERSONAL, GOLD

        // Home loan fields
        [Range(0.1, 100.0, ErrorMessage = "HomeInterestRate must be between 0.1 and 100")]
        public decimal? HomeInterestRate { get; set; }
        [Range(1, 600, ErrorMessage = "HomeTenureMonths must be between 1 and 600")]
        public int? HomeTenureMonths { get; set; }
        [Range(0, 1000000, ErrorMessage = "HomeProcessingFee must be between 0 and 1,000,000")]
        public decimal? HomeProcessingFee { get; set; }
        [Range(0, 100, ErrorMessage = "DownPaymentPercentage must be between 0 and 100")]
        public decimal? DownPaymentPercentage { get; set; }

        // Personal loan fields
        [Range(0.1, 100.0, ErrorMessage = "PersonalInterestRate must be between 0.1 and 100")]
        public decimal? PersonalInterestRate { get; set; }
        [Range(1, 600, ErrorMessage = "PersonalTenureMonths must be between 1 and 600")]
        public int? PersonalTenureMonths { get; set; }
        [Range(0, 1000000, ErrorMessage = "PersonalProcessingFee must be between 0 and 1,000,000")]
        public decimal? PersonalProcessingFee { get; set; }
        [Range(0, 10000000, ErrorMessage = "MinSalaryRequired must be between 0 and 10,000,000")]
        public decimal? MinSalaryRequired { get; set; }

        // Gold loan fields
        [Range(0.1, 100.0, ErrorMessage = "GoldInterestRate must be between 0.1 and 100")]
        public decimal? GoldInterestRate { get; set; }
        [Range(1, 600, ErrorMessage = "GoldTenureMonths must be between 1 and 600")]
        public int? GoldTenureMonths { get; set; }
        [Range(0, 1000000, ErrorMessage = "GoldProcessingFee must be between 0 and 1,000,000")]
        public decimal? GoldProcessingFee { get; set; }
        [StringLength(50, ErrorMessage = "GoldPurityRequired cannot exceed 50 characters")]
        public string GoldPurityRequired { get; set; }
        [StringLength(50, ErrorMessage = "RepaymentType cannot exceed 50 characters")]
        public string RepaymentType { get; set; }
    }
}