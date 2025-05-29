using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Core.DTOs.LoanProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using CredWiseAdmin.Service.Interfaces;
using CredWiseAdmin.Service.Exceptions;

namespace CredWiseAdmin.Service
{
    public class LoanProductService : ILoanProductService
    {
        private readonly ILoanProductRepository _loanProductRepository;

        public LoanProductService(ILoanProductRepository loanProductRepository)
        {
            _loanProductRepository = loanProductRepository ?? throw new ArgumentNullException(nameof(loanProductRepository));
        }

        public async Task<LoanProductListResponse> GetLoanProductsAsync()
        {
            var products = await _loanProductRepository.GetLoanProductsAsync();
            return new LoanProductListResponse
            {
                Success = true,
                Data = products.ToList(),
                Message = "Loan types fetched successfully"
            };
        }

        public async Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync(bool includeInactive)
        {
            return await _loanProductRepository.GetAllLoanProductsAsync(includeInactive);
        }

        public async Task<IEnumerable<LoanProduct>> GetAllLoanProductsAsync()
        {
            return await GetAllLoanProductsAsync(false);
        }

        public async Task<IEnumerable<LoanProduct>> GetActiveLoanProductsAsync()
        {
            return await _loanProductRepository.GetActiveLoanProductsAsync();
        }

        public async Task<IEnumerable<LoanProduct>> GetLoanProductsByTypeAsync(string loanType)
        {
            if (string.IsNullOrWhiteSpace(loanType))
                throw new BusinessException("Loan type cannot be empty");

            return await _loanProductRepository.GetLoanProductsByTypeAsync(loanType);
        }

        public async Task<LoanProduct> GetLoanProductByIdAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Invalid loan product ID");

            return await _loanProductRepository.GetByIdAsync(id);
        }

        public async Task<LoanProduct> GetLoanProductWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new BusinessException("Invalid loan product ID");

            return await _loanProductRepository.GetLoanProductWithDetailsAsync(id);
        }

        public async Task<LoanProduct> CreateLoanProductAsync(CreateLoanProductDto dto, string createdBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(createdBy))
                throw new BusinessException("Created by cannot be empty");

            // Validate base fields
            if (string.IsNullOrWhiteSpace(dto.Title) ||
                string.IsNullOrWhiteSpace(dto.Description) ||
                string.IsNullOrWhiteSpace(dto.ImageUrl) ||
                string.IsNullOrWhiteSpace(dto.LoanType))
                throw new BusinessException("Missing required base fields.");

            // Validate loan type specific fields
            switch (dto.LoanType.ToUpper())
            {
                case "HOME":
                    if (!dto.HomeInterestRate.HasValue || !dto.HomeTenureMonths.HasValue || 
                        !dto.HomeProcessingFee.HasValue || !dto.DownPaymentPercentage.HasValue)
                        throw new BusinessException("Missing required home loan fields.");
                    break;
                case "PERSONAL":
                    if (!dto.PersonalInterestRate.HasValue || !dto.PersonalTenureMonths.HasValue || 
                        !dto.PersonalProcessingFee.HasValue || !dto.MinSalaryRequired.HasValue)
                        throw new BusinessException("Missing required personal loan fields.");
                    break;
                case "GOLD":
                    if (!dto.GoldInterestRate.HasValue || !dto.GoldTenureMonths.HasValue || 
                        !dto.GoldProcessingFee.HasValue || string.IsNullOrWhiteSpace(dto.GoldPurityRequired) ||
                        string.IsNullOrWhiteSpace(dto.RepaymentType))
                        throw new BusinessException("Missing required gold loan fields.");
                    break;
                default:
                    throw new BusinessException("Invalid loan type.");
            }

            // 1. Create base loan product
            var loanProduct = new LoanProduct
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                MaxLoanAmount = dto.MaxLoanAmount,
                LoanType = dto.LoanType,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            var createdProduct = await _loanProductRepository.AddAsync(loanProduct);

            // 2. Create type-specific details
            switch (dto.LoanType.ToUpper())
            {
                case "HOME":
                    var homeDetail = new HomeLoanDetail
                    {
                        LoanProductId = createdProduct.LoanProductId,
                        InterestRate = dto.HomeInterestRate.Value,
                        TenureMonths = dto.HomeTenureMonths.Value,
                        ProcessingFee = dto.HomeProcessingFee.Value,
                        DownPaymentPercentage = dto.DownPaymentPercentage.Value,
                        CreatedBy = createdBy,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    await _loanProductRepository.AddHomeLoanDetailAsync(homeDetail);
                    break;
                case "PERSONAL":
                    var personalDetail = new PersonalLoanDetail
                    {
                        LoanProductId = createdProduct.LoanProductId,
                        InterestRate = dto.PersonalInterestRate.Value,
                        TenureMonths = dto.PersonalTenureMonths.Value,
                        ProcessingFee = dto.PersonalProcessingFee.Value,
                        MinSalaryRequired = dto.MinSalaryRequired.Value,
                        CreatedBy = createdBy,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    await _loanProductRepository.AddPersonalLoanDetailAsync(personalDetail);
                    break;
                case "GOLD":
                    var goldDetail = new GoldLoanDetail
                    {
                        LoanProductId = createdProduct.LoanProductId,
                        InterestRate = dto.GoldInterestRate.Value,
                        TenureMonths = dto.GoldTenureMonths.Value,
                        ProcessingFee = dto.GoldProcessingFee.Value,
                        GoldPurityRequired = dto.GoldPurityRequired,
                        RepaymentType = dto.RepaymentType,
                        CreatedBy = createdBy,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    await _loanProductRepository.AddGoldLoanDetailAsync(goldDetail);
                    break;
            }

            return createdProduct;
        }

        public async Task<LoanProduct> UpdateLoanProductAsync(LoanProduct loanProduct, string modifiedBy)
        {
            if (loanProduct == null)
                throw new ArgumentNullException(nameof(loanProduct));

            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new ArgumentException("Modified by cannot be empty", nameof(modifiedBy));

            // Get the existing product from the database
            var existingProduct = await _loanProductRepository.GetByIdAsync(loanProduct.LoanProductId);
            if (existingProduct == null)
                throw new NotFoundException($"Loan product with ID {loanProduct.LoanProductId} not found.");

            // Update fields
            existingProduct.Title = loanProduct.Title;
            existingProduct.Description = loanProduct.Description;
            existingProduct.ImageUrl = loanProduct.ImageUrl;
            existingProduct.MaxLoanAmount = loanProduct.MaxLoanAmount;
            existingProduct.LoanType = loanProduct.LoanType;
            existingProduct.IsActive = loanProduct.IsActive;
            existingProduct.ModifiedAt = DateTime.UtcNow;
            existingProduct.ModifiedBy = modifiedBy;

            // Save changes
            return await _loanProductRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteLoanProductAsync(int id, string modifiedBy)
        {
            if (id <= 0)
                throw new BusinessException("Invalid loan product ID");

            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new BusinessException("Modified by cannot be empty");

            var loanProduct = await _loanProductRepository.GetByIdAsync(id);
            if (loanProduct == null)
                throw new NotFoundException($"Loan product with ID {id} not found.");

            await _loanProductRepository.DeleteAsync(id);
        }

        public async Task<bool> IsLoanProductNameUniqueAsync(string title, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessException("Title cannot be empty");

            return await _loanProductRepository.IsLoanProductNameUniqueAsync(title, excludeId);
        }

        private void ValidateLoanProduct(LoanProduct loanProduct)
        {
            if (string.IsNullOrWhiteSpace(loanProduct.Title))
                throw new BusinessException("Title is required");

            if (string.IsNullOrWhiteSpace(loanProduct.Description))
                throw new BusinessException("Description is required");

            if (string.IsNullOrWhiteSpace(loanProduct.ImageUrl))
                throw new BusinessException("Image URL is required");

            if (loanProduct.MaxLoanAmount <= 0)
                throw new BusinessException("Maximum loan amount must be greater than zero");

            if (string.IsNullOrWhiteSpace(loanProduct.LoanType))
                throw new BusinessException("Loan type is required");

            if (!IsValidLoanType(loanProduct.LoanType))
                throw new BusinessException("Invalid loan type");
        }

        private bool IsValidLoanType(string loanType)
        {
            return loanType.ToUpper() switch
            {
                "HOME" => true,
                "PERSONAL" => true,
                "GOLD" => true,
                _ => false
            };
        }

        public async Task<LoanProduct> CreateHomeLoanProductAsync(CreateHomeLoanProductDto dto, string createdBy)
        {
            return await _loanProductRepository.CreateHomeLoanProductAsync(dto, createdBy);
        }

        public async Task<LoanProduct> CreatePersonalLoanProductAsync(CreatePersonalLoanProductDto dto, string createdBy)
        {
            return await _loanProductRepository.CreatePersonalLoanProductAsync(dto, createdBy);
        }

        public async Task<LoanProduct> CreateGoldLoanProductAsync(CreateGoldLoanProductDto dto, string createdBy)
        {
            return await _loanProductRepository.CreateGoldLoanProductAsync(dto, createdBy);
        }

        public async Task<LoanProduct> UpdatePersonalLoanProductAsync(int id, UpdatePersonalLoanProductDto dto, string modifiedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new ArgumentException("Modified by cannot be empty", nameof(modifiedBy));

            // Fetch the base loan product
            var loanProduct = await _loanProductRepository.GetByIdAsync(id);
            if (loanProduct == null)
                throw new NotFoundException($"Loan product with ID {id} not found.");
            if (loanProduct.LoanType.ToUpper() != "PERSONAL")
                throw new BusinessException("Loan product is not of type PERSONAL.");

            // Update base fields
            loanProduct.Title = dto.Title;
            loanProduct.Description = dto.Description;
            loanProduct.ImageUrl = dto.ImageUrl;
            loanProduct.MaxLoanAmount = dto.MaxLoanAmount;
            loanProduct.LoanType = dto.LoanType;
            loanProduct.ModifiedAt = DateTime.UtcNow;
            loanProduct.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdateAsync(loanProduct);

            // Fetch and update personal loan detail
            var personalDetail = await _loanProductRepository.GetPersonalLoanDetailAsync(id);
            if (personalDetail == null)
                throw new NotFoundException($"Personal loan detail for product ID {id} not found.");

            personalDetail.InterestRate = dto.InterestRate;
            personalDetail.TenureMonths = dto.TenureMonths;
            personalDetail.ProcessingFee = dto.ProcessingFee;
            personalDetail.MinSalaryRequired = dto.MinSalaryRequired;
            personalDetail.ModifiedAt = DateTime.UtcNow;
            personalDetail.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdatePersonalLoanDetailAsync(personalDetail);

            return loanProduct;
        }

        public async Task<LoanProduct> UpdateHomeLoanProductAsync(int id, UpdateHomeLoanProductDto dto, string modifiedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new ArgumentException("Modified by cannot be empty", nameof(modifiedBy));

            // Fetch the base loan product
            var loanProduct = await _loanProductRepository.GetByIdAsync(id);
            if (loanProduct == null)
                throw new NotFoundException($"Loan product with ID {id} not found.");
            if (loanProduct.LoanType.ToUpper() != "HOME")
                throw new BusinessException("Loan product is not of type HOME.");

            // Update base fields
            loanProduct.Title = dto.Title;
            loanProduct.Description = dto.Description;
            loanProduct.ImageUrl = dto.ImageUrl;
            loanProduct.MaxLoanAmount = dto.MaxLoanAmount;
            loanProduct.LoanType = dto.LoanType;
            loanProduct.ModifiedAt = DateTime.UtcNow;
            loanProduct.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdateAsync(loanProduct);

            // Fetch and update home loan detail
            var homeDetail = await _loanProductRepository.GetHomeLoanDetailAsync(id);
            if (homeDetail == null)
                throw new NotFoundException($"Home loan detail for product ID {id} not found.");

            homeDetail.InterestRate = dto.InterestRate;
            homeDetail.TenureMonths = dto.TenureMonths;
            homeDetail.ProcessingFee = dto.ProcessingFee;
            homeDetail.DownPaymentPercentage = dto.DownPaymentPercentage;
            homeDetail.ModifiedAt = DateTime.UtcNow;
            homeDetail.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdateHomeLoanDetailAsync(homeDetail);

            return loanProduct;
        }

        public async Task<LoanProduct> UpdateGoldLoanProductAsync(int id, UpdateGoldLoanProductDto dto, string modifiedBy)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(modifiedBy))
                throw new ArgumentException("Modified by cannot be empty", nameof(modifiedBy));

            // Fetch the base loan product
            var loanProduct = await _loanProductRepository.GetByIdAsync(id);
            if (loanProduct == null)
                throw new NotFoundException($"Loan product with ID {id} not found.");
            if (loanProduct.LoanType.ToUpper() != "GOLD")
                throw new BusinessException("Loan product is not of type GOLD.");

            // Update base fields
            loanProduct.Title = dto.Title;
            loanProduct.Description = dto.Description;
            loanProduct.ImageUrl = dto.ImageUrl;
            loanProduct.MaxLoanAmount = dto.MaxLoanAmount;
            loanProduct.LoanType = dto.LoanType;
            loanProduct.ModifiedAt = DateTime.UtcNow;
            loanProduct.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdateAsync(loanProduct);

            // Fetch and update gold loan detail
            var goldDetail = await _loanProductRepository.GetGoldLoanDetailAsync(id);
            if (goldDetail == null)
                throw new NotFoundException($"Gold loan detail for product ID {id} not found.");

            goldDetail.InterestRate = dto.InterestRate;
            goldDetail.TenureMonths = dto.TenureMonths;
            goldDetail.ProcessingFee = dto.ProcessingFee;
            goldDetail.GoldPurityRequired = dto.GoldPurityRequired;
            goldDetail.RepaymentType = dto.RepaymentType;
            goldDetail.ModifiedAt = DateTime.UtcNow;
            goldDetail.ModifiedBy = modifiedBy;

            await _loanProductRepository.UpdateGoldLoanDetailAsync(goldDetail);

            return loanProduct;
        }
    }
} 