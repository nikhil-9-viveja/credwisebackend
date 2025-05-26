using System;
using System.Collections.Generic;

namespace CredWiseAdmin.Core.DTOs.LoanProduct
{
    public class LoanProductDetailDto : LoanProductDto
    {
        public List<LoanProductFeatureDto> Features { get; set; }
        public List<LoanProductDocumentDto> RequiredDocuments { get; set; }
        public List<LoanProductEligibilityDto> EligibilityCriteria { get; set; }
    }

    public class LoanProductFeatureDto
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class LoanProductDocumentDto
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class LoanProductEligibilityDto
    {
        public int EligibilityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
    }
} 