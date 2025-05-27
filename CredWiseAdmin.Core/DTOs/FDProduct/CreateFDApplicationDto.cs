using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class CreateFDApplicationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }      // User's email for lookup

        [Required(ErrorMessage = "FD Type ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid FD Type ID")]
        public int FDTypeId { get; set; }      // Selected FD type (ID)

        [Required(ErrorMessage = "Amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Amount must be between 1,000 and 10,000,000")]
        public decimal Amount { get; set; }
    }
} 