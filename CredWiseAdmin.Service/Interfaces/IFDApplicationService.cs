using CredWiseAdmin.Core.DTOs.FDProduct;

namespace CredWiseAdmin.Service.Interfaces
{
    public interface IFDApplicationService
    {
        Task<FDApplicationResponseDto> CreateFDApplicationAsync(CreateFDApplicationDto dto, string createdBy);
        Task<FDApplicationResponseDto> UpdateFDApplicationAsync(UpdateFDApplicationDto dto, string modifiedBy);
        Task<bool> DeleteFDApplicationAsync(int fdapplicationId, string modifiedBy);
        Task<FDApplicationResponseDto?> GetFDApplicationByIdAsync(int fdapplicationId);
        Task<IEnumerable<FDApplicationResponseDto>> GetAllFDApplicationsAsync();
    }
} 