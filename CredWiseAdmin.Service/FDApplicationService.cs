using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;

namespace CredWiseAdmin.Service
{
    public class FDApplicationService : IFDApplicationService
    {
        private readonly IFDApplicationRepository _fdApplicationRepository;

        public FDApplicationService(IFDApplicationRepository fdApplicationRepository)
        {
            _fdApplicationRepository = fdApplicationRepository;
        }

        public async Task<FDApplicationResponseDto> CreateFDApplicationAsync(CreateFDApplicationDto dto, string createdBy)
        {
            var fdApplication = new Fdapplication
            {
                UserId = dto.UserId,
                FdtypeId = dto.FdtypeId,
                Amount = dto.Amount,
                Duration = dto.Duration,
                InterestRate = dto.InterestRate,
                Status = "Pending",
                IsActive = true,
                CreatedBy = createdBy
            };

            var result = await _fdApplicationRepository.AddAsync(fdApplication);
            return MapToResponseDto(result);
        }

        public async Task<FDApplicationResponseDto> UpdateFDApplicationAsync(UpdateFDApplicationDto dto, string modifiedBy)
        {
            var fdApplication = await _fdApplicationRepository.GetByIdAsync(dto.FdapplicationId);
            if (fdApplication == null)
                return null;

            fdApplication.UserId = dto.UserId;
            fdApplication.FdtypeId = dto.FdtypeId;
            fdApplication.Amount = dto.Amount;
            fdApplication.Duration = dto.Duration;
            fdApplication.InterestRate = dto.InterestRate;
            fdApplication.Status = dto.Status;
            fdApplication.ModifiedBy = modifiedBy;

            var result = await _fdApplicationRepository.UpdateAsync(fdApplication);
            return MapToResponseDto(result);
        }

        public async Task<bool> DeleteFDApplicationAsync(int fdapplicationId, string modifiedBy)
        {
            var fdApplication = await _fdApplicationRepository.GetByIdAsync(fdapplicationId);
            if (fdApplication == null)
                return false;

            await _fdApplicationRepository.DeleteAsync(fdapplicationId);
            return true;
        }

        public async Task<FDApplicationResponseDto?> GetFDApplicationByIdAsync(int fdapplicationId)
        {
            var fdApplication = await _fdApplicationRepository.GetByIdAsync(fdapplicationId);
            return fdApplication != null ? MapToResponseDto(fdApplication) : null;
        }

        public async Task<IEnumerable<FDApplicationResponseDto>> GetAllFDApplicationsAsync()
        {
            var fdApplications = await _fdApplicationRepository.GetAllAsync();
            return fdApplications.Select(MapToResponseDto);
        }

        private FDApplicationResponseDto MapToResponseDto(Fdapplication fdApplication)
        {
            return new FDApplicationResponseDto
            {
                FdapplicationId = fdApplication.FdapplicationId,
                UserId = fdApplication.UserId,
                FdtypeId = fdApplication.FdtypeId,
                Amount = fdApplication.Amount,
                Duration = fdApplication.Duration,
                InterestRate = fdApplication.InterestRate,
                Status = fdApplication.Status,
                IsActive = fdApplication.IsActive,
                CreatedBy = fdApplication.CreatedBy,
                ModifiedBy = fdApplication.ModifiedBy
            };
        }
    }
} 