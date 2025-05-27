using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class UpdateFDApplicationDto
    {
        [Required(ErrorMessage = "FD Application ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid FD Application ID")]
        public int FdapplicationId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid User ID")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FD Type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid FD Type ID")]
        public int FdtypeId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Amount must be between 1,000 and 10,000,000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [RegularExpression("^(12|36)$", ErrorMessage = "Duration must be 12 (1 year) or 36 (3 years) months")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Interest rate is required")]
        [Range(0.1, 100.0, ErrorMessage = "Interest rate must be between 0.1 and 100")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Pending|Approved|Rejected|Active|Matured|PrematureClosed)$", 
            ErrorMessage = "Status must be one of: Pending, Approved, Rejected, Active, Matured, PrematureClosed")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; }

        public DateTime? MaturityDate { get; set; }
        public decimal? MaturityAmount { get; set; }
        public bool IsActive { get; set; }
    }
} 