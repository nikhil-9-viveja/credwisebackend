namespace CredWiseAdmin.Core.DTOs.FDProduct
{
    public class FDTypeResponseDto
    {
        public int FdtypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
} 