using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Core.DTOs;
using CredWiseAdmin.Core.DTOs.LoanProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Service.Interfaces
{
    public interface ILoanProductService
    {
        Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync();
        Task<IEnumerable<LoanProduct>> GetActiveLoanProductsAsync();
        Task<IEnumerable<LoanProduct>> GetLoanProductsByTypeAsync(string loanType);
        Task<LoanProduct> GetLoanProductByIdAsync(int id);
        Task<LoanProduct> GetLoanProductWithDetailsAsync(int id);
        Task<LoanProduct> CreateLoanProductAsync(CreateLoanProductDto dto, string createdBy);
        Task<LoanProduct> UpdateLoanProductAsync(LoanProduct loanProduct, string modifiedBy);
        Task DeleteLoanProductAsync(int id, string modifiedBy);
        Task<bool> IsLoanProductNameUniqueAsync(string title, int? excludeId = null);
        Task<LoanProductListResponse> GetLoanProductsAsync();
        Task<LoanProduct> CreateHomeLoanProductAsync(CreateHomeLoanProductDto dto, string createdBy);
        Task<LoanProduct> CreatePersonalLoanProductAsync(CreatePersonalLoanProductDto dto, string createdBy);
        Task<LoanProduct> CreateGoldLoanProductAsync(CreateGoldLoanProductDto dto, string createdBy);
        Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync(bool includeInactive);
    }
} 