using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class CreateFDTransactionDto
    {
        [Required]
        public int FdapplicationId { get; set; }

        [Required]
        [RegularExpression("^(Deposit|InterestPayout|MaturityPayout|PrematureWithdrawal|Refund)$", ErrorMessage = "Invalid transaction type.")]
        public string TransactionType { get; set; }

        [Required]
        [Range(0.01, 10000000, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
    }
} 