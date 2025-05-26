using System;
using System.Collections.Generic;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class LoanProductDetailResponseDto : LoanProductResponseDto
    {
        public List<LoanProductDocumentResponseDto> RequiredDocuments { get; set; }
    }

    public class LoanProductDocumentResponseDto
    {
        public int LoanProductDocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
} 