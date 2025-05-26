using System.Collections.Generic;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class HomeLoanDetailDto
    {
        public decimal InterestRate { get; set; }
        public int TenureMonths { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal DownPaymentPercentage { get; set; }
        public string RepaymentType { get; set; }
        public List<string> DocumentsRequired { get; set; } = new List<string>
        {
            "PAN Card",
            "Aadhaar Card",
            "Income Proof",
            "Property Papers"
        };
    }
} 