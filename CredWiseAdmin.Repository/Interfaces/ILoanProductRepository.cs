using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using CredWiseAdmin.Core.DTOs.LoanProduct;

namespace CredWiseAdmin.Repository.Interfaces
{
    public interface ILoanProductRepository : IGenericRepository<LoanProduct>
    {
        Task<IEnumerable<LoanProduct>> GetActiveLoanProductsAsync();
        Task<IEnumerable<LoanProduct>> GetLoanProductsByTypeAsync(string loanType);
        Task<LoanProduct> GetLoanProductWithDetailsAsync(int id);
        Task<bool> IsLoanProductNameUniqueAsync(string title, int? excludeId = null);
        Task AddHomeLoanDetailAsync(HomeLoanDetail detail);
        Task AddPersonalLoanDetailAsync(PersonalLoanDetail detail);
        Task AddGoldLoanDetailAsync(GoldLoanDetail detail);
        Task UpdateHomeLoanDetailAsync(HomeLoanDetail detail);
        Task UpdatePersonalLoanDetailAsync(PersonalLoanDetail detail);
        Task UpdateGoldLoanDetailAsync(GoldLoanDetail detail);
        Task<HomeLoanDetail> GetHomeLoanDetailAsync(int loanProductId);
        Task<PersonalLoanDetail> GetPersonalLoanDetailAsync(int loanProductId);
        Task<GoldLoanDetail> GetGoldLoanDetailAsync(int loanProductId);
        Task<LoanProduct> CreateLoanProductAsync(CreateLoanProductDto dto, string createdBy);
        Task<IEnumerable<LoanProductResponseDto>> GetLoanProductsAsync();
        Task<LoanProduct> CreateHomeLoanProductAsync(CreateHomeLoanProductDto dto, string createdBy);
        Task<LoanProduct> CreatePersonalLoanProductAsync(CreatePersonalLoanProductDto dto, string createdBy);
        Task<LoanProduct> CreateGoldLoanProductAsync(CreateGoldLoanProductDto dto, string createdBy);
        Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync(bool includeInactive);
    }
} 