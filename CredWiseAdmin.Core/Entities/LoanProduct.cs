using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CredWiseAdmin.Core.Entities;

public class LoanProduct : BaseEntity
{
    [Key]
    public int LoanProductId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal MaxLoanAmount { get; set; }

    [Required]
    [MaxLength(20)]
    public string LoanType { get; set; }

    [InverseProperty("LoanProduct")]
    public virtual GoldLoanDetail? GoldLoanDetail { get; set; }

    [InverseProperty("LoanProduct")]
    public virtual HomeLoanDetail? HomeLoanDetail { get; set; }

    [InverseProperty("LoanProduct")]
    public virtual ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();

    [InverseProperty("LoanProduct")]
    public virtual ICollection<LoanProductDocument> LoanProductDocuments { get; set; } = new List<LoanProductDocument>();

    [InverseProperty("LoanProduct")]
    public virtual PersonalLoanDetail? PersonalLoanDetail { get; set; }
}
