using System;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class LoanProductDto
    {
        public int LoanProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public string LoanType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }

   
} 