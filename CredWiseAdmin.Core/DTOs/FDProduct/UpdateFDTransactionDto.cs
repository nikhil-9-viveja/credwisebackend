using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class UpdateFDTransactionDto
    {
        [Required]
        public int FdtransactionId { get; set; }

        [Required]
        public int FdapplicationId { get; set; }

        [Required]
        [RegularExpression("^(Deposit|InterestPayout|MaturityPayout|PrematureWithdrawal|Refund)$", 
            ErrorMessage = "TransactionType must be one of: Deposit, InterestPayout, MaturityPayout, PrematureWithdrawal, Refund")]
        public string TransactionType { get; set; }

        [Required]
        [Range(0.01, 10000000, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [RegularExpression("^(NEFT|RTGS|IMPS|UPI|CASH|CHEQUE)$", 
            ErrorMessage = "PaymentMethod must be one of: NEFT, RTGS, IMPS, UPI, CASH, CHEQUE")]
        public string PaymentMethod { get; set; }

        [Required]
        [RegularExpression("^(Success|Failed|Pending)$", ErrorMessage = "TransactionStatus must be one of: Success, Failed, Pending")]
        public string TransactionStatus { get; set; }

        [StringLength(100)]
        public string? TransactionReference { get; set; }
        public bool IsActive { get; set; }
    }
} 