using System;
using System.Collections.Generic;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class LoanProductResponseDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public string LoanType { get; set; }
        public object LoanDetail { get; set; } // Will be HomeLoanDetailDto, PersonalLoanDetailDto, or GoldLoanDetailDto
        public bool IsActive { get; set; }
    }

    public class LoanProductListResponse
    {
        public bool Success { get; set; }
        public List<LoanProductResponseDto> Data { get; set; }
        public string Message { get; set; }
    }

    // public class UpdateLoanProductStatusDto
    // {
    //     public int LoanProductId { get; set; }
    //     public bool IsActive { get; set; }
    // }
} 