using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using CredWiseAdmin.Core.DTOs.LoanProduct;

namespace CredWiseAdmin.Repository
{
    public class LoanProductRepository : GenericRepository<LoanProduct>, ILoanProductRepository
    {
        private new readonly AppDbContext _context;

        public LoanProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanProduct>> GetActiveLoanProductsAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.HomeLoanDetail)
                .Include(x => x.PersonalLoanDetail)
                .Include(x => x.GoldLoanDetail)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanProduct>> GetLoanProductsByTypeAsync(string loanType)
        {
            return await _dbSet
                .Where(x => x.IsActive && x.LoanType == loanType)
                .Include(x => x.HomeLoanDetail)
                .Include(x => x.PersonalLoanDetail)
                .Include(x => x.GoldLoanDetail)
                .ToListAsync();
        }

        public async Task<LoanProduct> GetLoanProductWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(x => x.HomeLoanDetail)
                .Include(x => x.PersonalLoanDetail)
                .Include(x => x.GoldLoanDetail)
                .Include(x => x.LoanProductDocuments)
                .FirstOrDefaultAsync(x => x.LoanProductId == id && x.IsActive);
        }

        public async Task<bool> IsLoanProductNameUniqueAsync(string title, int? excludeId = null)
        {
            return !await _dbSet
                .AnyAsync(x => x.Title == title && 
                              x.IsActive && 
                              (!excludeId.HasValue || x.LoanProductId != excludeId.Value));
        }

        public async Task AddHomeLoanDetailAsync(HomeLoanDetail detail)
        {
            var now = DateTime.UtcNow;
            detail.CreatedAt = detail.CreatedAt == default ? now : detail.CreatedAt;
            detail.ModifiedAt = now;
            detail.ModifiedBy = detail.CreatedBy;
            await _context.HomeLoanDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task AddPersonalLoanDetailAsync(PersonalLoanDetail detail)
        {
            var now = DateTime.UtcNow;
            detail.CreatedAt = detail.CreatedAt == default ? now : detail.CreatedAt;
            detail.ModifiedAt = now;
            detail.ModifiedBy = detail.CreatedBy;
            await _context.PersonalLoanDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task AddGoldLoanDetailAsync(GoldLoanDetail detail)
        {
            var now = DateTime.UtcNow;
            detail.CreatedAt = detail.CreatedAt == default ? now : detail.CreatedAt;
            detail.ModifiedAt = now;
            detail.ModifiedBy = detail.CreatedBy;
            await _context.GoldLoanDetails.AddAsync(detail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHomeLoanDetailAsync(HomeLoanDetail detail)
        {
            _context.HomeLoanDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalLoanDetailAsync(PersonalLoanDetail detail)
        {
            _context.PersonalLoanDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGoldLoanDetailAsync(GoldLoanDetail detail)
        {
            _context.GoldLoanDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task<HomeLoanDetail> GetHomeLoanDetailAsync(int loanProductId)
        {
            return await _context.HomeLoanDetails.FirstOrDefaultAsync(x => x.LoanProductId == loanProductId);
        }

        public async Task<PersonalLoanDetail> GetPersonalLoanDetailAsync(int loanProductId)
        {
            return await _context.PersonalLoanDetails.FirstOrDefaultAsync(x => x.LoanProductId == loanProductId);
        }

        public async Task<GoldLoanDetail> GetGoldLoanDetailAsync(int loanProductId)
        {
            return await _context.GoldLoanDetails.FirstOrDefaultAsync(x => x.LoanProductId == loanProductId);
        }

        public async Task<LoanProduct> CreateLoanProductAsync(CreateLoanProductDto dto, string createdBy)
        {
            var loanProduct = new LoanProduct
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                MaxLoanAmount = dto.MaxLoanAmount,
                LoanType = dto.LoanType,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                ModifiedBy = createdBy,
                IsActive = true
            };

            await _context.LoanProducts.AddAsync(loanProduct);
            await _context.SaveChangesAsync();

            // Add type-specific details if needed (Home, Personal, Gold)
            // ... (implement as per your business logic)

            return loanProduct;
        }

        public async Task<IEnumerable<LoanProductResponseDto>> GetLoanProductsAsync()
        {
            var loanProducts = await _context.LoanProducts
                .Where(lp => lp.IsActive)
                .ToListAsync();

            var response = new List<LoanProductResponseDto>();

            foreach (var product in loanProducts)
            {
                object loanDetail = null;
                
                switch (product.LoanType.ToUpper())
                {
                    case "HOME":
                        var homeDetail = await _context.HomeLoanDetails
                            .FirstOrDefaultAsync(h => h.LoanProductId == product.LoanProductId);
                        if (homeDetail != null)
                        {
                            loanDetail = new HomeLoanDetailDto
                            {
                                InterestRate = homeDetail.InterestRate,
                                TenureMonths = homeDetail.TenureMonths,
                                ProcessingFee = homeDetail.ProcessingFee,
                                DownPaymentPercentage = homeDetail.DownPaymentPercentage,
                                RepaymentType = "EMI"
                            };
                        }
                        break;

                    case "PERSONAL":
                        var personalDetail = await _context.PersonalLoanDetails
                            .FirstOrDefaultAsync(p => p.LoanProductId == product.LoanProductId);
                        if (personalDetail != null)
                        {
                            loanDetail = new PersonalLoanDetailDto
                            {
                                InterestRate = personalDetail.InterestRate,
                                TenureMonths = personalDetail.TenureMonths,
                                ProcessingFee = personalDetail.ProcessingFee,
                                MinSalaryRequired = personalDetail.MinSalaryRequired,
                                RepaymentType = "EMI"
                            };
                        }
                        break;

                    case "GOLD":
                        var goldDetail = await _context.GoldLoanDetails
                            .FirstOrDefaultAsync(g => g.LoanProductId == product.LoanProductId);
                        if (goldDetail != null)
                        {
                            loanDetail = new GoldLoanDetailDto
                            {
                                InterestRate = goldDetail.InterestRate,
                                TenureMonths = goldDetail.TenureMonths,
                                ProcessingFee = goldDetail.ProcessingFee,
                                GoldPurityRequired = goldDetail.GoldPurityRequired,
                                RepaymentType = goldDetail.RepaymentType
                            };
                        }
                        break;
                }

                response.Add(new LoanProductResponseDto
                {
                    Id = product.LoanProductId,
                    Image = product.ImageUrl,
                    Title = product.Title,
                    Description = product.Description,
                    MaxLoanAmount = product.MaxLoanAmount,
                    LoanType = product.LoanType,
                    LoanDetail = loanDetail
                });
            }

            return response;
        }

        public async Task<LoanProduct> CreateHomeLoanProductAsync(CreateHomeLoanProductDto dto, string createdBy)
        {
            var now = DateTime.UtcNow;
            var loanProduct = new LoanProduct
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                MaxLoanAmount = dto.MaxLoanAmount,
                LoanType = "HOME",
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.LoanProducts.AddAsync(loanProduct);
            await _context.SaveChangesAsync();

            var homeDetail = new HomeLoanDetail
            {
                LoanProductId = loanProduct.LoanProductId,
                InterestRate = dto.InterestRate,
                TenureMonths = dto.TenureMonths,
                ProcessingFee = dto.ProcessingFee,
                DownPaymentPercentage = dto.DownPaymentPercentage,
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.HomeLoanDetails.AddAsync(homeDetail);
            await _context.SaveChangesAsync();

            return loanProduct;
        }

        public async Task<LoanProduct> CreatePersonalLoanProductAsync(CreatePersonalLoanProductDto dto, string createdBy)
        {
            var now = DateTime.UtcNow;
            var loanProduct = new LoanProduct
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                MaxLoanAmount = dto.MaxLoanAmount,
                LoanType = "PERSONAL",
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.LoanProducts.AddAsync(loanProduct);
            await _context.SaveChangesAsync();

            var personalDetail = new PersonalLoanDetail
            {
                LoanProductId = loanProduct.LoanProductId,
                InterestRate = dto.InterestRate,
                TenureMonths = dto.TenureMonths,
                ProcessingFee = dto.ProcessingFee,
                MinSalaryRequired = dto.MinSalaryRequired,
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.PersonalLoanDetails.AddAsync(personalDetail);
            await _context.SaveChangesAsync();

            return loanProduct;
        }

        public async Task<LoanProduct> CreateGoldLoanProductAsync(CreateGoldLoanProductDto dto, string createdBy)
        {
            var now = DateTime.UtcNow;
            var loanProduct = new LoanProduct
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                MaxLoanAmount = dto.MaxLoanAmount,
                LoanType = "GOLD",
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.LoanProducts.AddAsync(loanProduct);
            await _context.SaveChangesAsync();

            var goldDetail = new GoldLoanDetail
            {
                LoanProductId = loanProduct.LoanProductId,
                InterestRate = dto.InterestRate,
                TenureMonths = dto.TenureMonths,
                ProcessingFee = dto.ProcessingFee,
                GoldPurityRequired = dto.GoldPurityRequired,
                RepaymentType = dto.RepaymentType,
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedAt = now,
                ModifiedBy = createdBy,
                IsActive = true
            };
            await _context.GoldLoanDetails.AddAsync(goldDetail);
            await _context.SaveChangesAsync();

            return loanProduct;
        }
    }
} 