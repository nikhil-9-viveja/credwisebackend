namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class FDApplicationResponseDto
    {
        public int FdapplicationId { get; set; }
        public int UserId { get; set; }
        public int FdtypeId { get; set; }
        public string? FDTypeName { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal InterestRate { get; set; }
        public string Status { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? MaturityAmount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
} 