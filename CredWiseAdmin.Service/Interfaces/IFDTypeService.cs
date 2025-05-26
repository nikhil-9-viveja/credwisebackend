using CredWiseAdmin.Core.DTOs.FDProduct;

namespace CredWiseAdmin.Service.Interfaces
{
    public interface IFDTypeService
    {
        Task<FDTypeResponseDto> CreateFDTypeAsync(CreateFDTypeDto dto, string createdBy);
        Task<FDTypeResponseDto> UpdateFDTypeAsync(UpdateFDTypeDto dto, string modifiedBy);
        Task<bool> DeleteFDTypeAsync(int fdtypeId, string modifiedBy);
        Task<FDTypeResponseDto?> GetFDTypeByIdAsync(int fdtypeId);
        Task<IEnumerable<FDTypeResponseDto>> GetAllFDTypesAsync();
    }
} 