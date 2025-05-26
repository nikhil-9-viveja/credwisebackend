namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class FDTransactionResponseDto
    {
        public int FdtransactionId { get; set; }
        public int FdapplicationId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionStatus { get; set; }
        public string? TransactionReference { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
} 