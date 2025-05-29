using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;
using AutoMapper;
using System.Linq;

namespace CredWiseAdmin.Service
{
    public class FDTypeService : IFDTypeService
    {
        private readonly IFDTypeRepository _fdTypeRepository;
        private readonly IMapper _mapper;

        public FDTypeService(IFDTypeRepository fdTypeRepository, IMapper mapper)
        {
            _fdTypeRepository = fdTypeRepository;
            _mapper = mapper;
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
                return _mapper.Map<FDTypeResponseDto>(result);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\temp\\fdtype_error.txt", (ex.InnerException?.Message ?? ex.Message) + System.Environment.NewLine);
                throw;
            }
        }

        public async Task<FDTypeResponseDto> UpdateFDTypeAsync(UpdateFDTypeDto dto, string modifiedBy)
        {
            var fdType = await _fdTypeRepository.GetByIdAsync(dto.FdtypeId);
            if (fdType == null)
                return null;

            fdType.Name = dto.Name;
            fdType.Description = dto.Description;
            fdType.InterestRate = dto.InterestRate;
            fdType.MinAmount = dto.MinAmount;
            fdType.MaxAmount = dto.MaxAmount;
            fdType.Duration = dto.Duration;
            fdType.IsActive = dto.IsActive;
            fdType.ModifiedBy = modifiedBy;
            fdType.ModifiedAt = DateTime.UtcNow;

            try
            {
                var result = await _fdTypeRepository.UpdateAsync(fdType);
                return _mapper.Map<FDTypeResponseDto>(result);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\temp\\fdtype_error.txt", (ex.InnerException?.Message ?? ex.Message) + System.Environment.NewLine);
                throw;
            }
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
            return fdType != null ? _mapper.Map<FDTypeResponseDto>(fdType) : null;
        }

        public async Task<IEnumerable<FDTypeResponseDto>> GetAllFDTypesAsync()
        {
            var fdTypes = await _fdTypeRepository.GetAllAsync();
            return fdTypes.Select(_mapper.Map<FDTypeResponseDto>);
        }

        public async Task<FDTypeResponseDto?> ToggleFDTypeStatusAsync(int fdtypeId, string modifiedBy)
        {
            var fdType = await _fdTypeRepository.GetByIdAsync(fdtypeId);
            if (fdType == null)
                return null;

            fdType.IsActive = !fdType.IsActive;
            fdType.ModifiedBy = modifiedBy;
            fdType.ModifiedAt = DateTime.UtcNow;

            var updated = await _fdTypeRepository.UpdateAsync(fdType);
            return _mapper.Map<FDTypeResponseDto>(updated);
        }
    }
} 