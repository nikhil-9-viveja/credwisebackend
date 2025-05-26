using System;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class LoanProductEligibilityResponseDto
    {
        public int EligibilityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
    }
} 