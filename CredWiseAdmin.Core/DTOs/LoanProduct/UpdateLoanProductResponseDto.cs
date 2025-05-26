using System;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class UpdateLoanProductResponseDto
    {
        public int LoanProductId { get; set; }
        public string Title { get; set; }
        public string LoanType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
} 