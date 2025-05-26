using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;

namespace CredWiseAdmin.Service
{
    public class FDTypeService : IFDTypeService
    {
        private readonly IFDTypeRepository _fdTypeRepository;

        public FDTypeService(IFDTypeRepository fdTypeRepository)
        {
            _fdTypeRepository = fdTypeRepository;
        }

        public async Task<FDTypeResponseDto> CreateFDTypeAsync(CreateFDTypeDto dto, string createdBy)
        {
            var now = DateTime.UtcNow;
            var fdType = new Fdtype
            {
                Name = dto.Name,
                Description = dto.Description,
                InterestRate = dto.InterestRate,
                MinAmount = dto.MinAmount,
                MaxAmount = dto.MaxAmount,
                Duration = dto.Duration,
                IsActive = true,
                CreatedAt = now,
                CreatedBy = createdBy,
                ModifiedAt = now,
                ModifiedBy = createdBy
            };

            try
            {
                var result = await _fdTypeRepository.AddAsync(fdType);
                return MapToResponseDto(result);
            }
            catch (Exception ex)
            {
                // Print to console or log file
                Console.WriteLine("EXCEPTION: " + (ex.InnerException?.Message ?? ex.Message));
                throw; // rethrow so you still see the error in the API
            }
        }

        public async Task<FDTypeResponseDto> UpdateFDTypeAsync(UpdateFDTypeDto dto, string modifiedBy)
        {
            var fdType = await _fdTypeRepository.GetByIdAsync(dto.FdtypeId);
            if (fdType == null)
                return null;

            fdType.Name = dto.Name;
            fdType.Description = dto.Description;
            fdType.ModifiedBy = modifiedBy;

            var result = await _fdTypeRepository.UpdateAsync(fdType);
            return MapToResponseDto(result);
        }

        public async Task<bool> DeleteFDTypeAsync(int fdtypeId, string modifiedBy)
        {
            var fdType = await _fdTypeRepository.GetByIdAsync(fdtypeId);
            if (fdType == null)
                return false;

            await _fdTypeRepository.DeleteAsync(fdtypeId);
            return true;
        }

        public async Task<FDTypeResponseDto?> GetFDTypeByIdAsync(int fdtypeId)
        {
            var fdType = await _fdTypeRepository.GetByIdAsync(fdtypeId);
            return fdType != null ? MapToResponseDto(fdType) : null;
        }

        public async Task<IEnumerable<FDTypeResponseDto>> GetAllFDTypesAsync()
        {
            var fdTypes = await _fdTypeRepository.GetAllAsync();
            return fdTypes.Select(MapToResponseDto);
        }

        private FDTypeResponseDto MapToResponseDto(Fdtype fdType)
        {
            return new FDTypeResponseDto
            {
                FdtypeId = fdType.FdtypeId,
                Name = fdType.Name,
                Description = fdType.Description,
                IsActive = fdType.IsActive,
                CreatedBy = fdType.CreatedBy,
                ModifiedBy = fdType.ModifiedBy
            };
        }
    }
} 