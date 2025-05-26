using CredWiseAdmin.Core.DTOs.FDProduct;

namespace CredWiseAdmin.Service.Interfaces
{
    public interface IFDTransactionService
    {
        Task<FDTransactionResponseDto> CreateFDTransactionAsync(CreateFDTransactionDto dto, string createdBy);
        Task<FDTransactionResponseDto> UpdateFDTransactionAsync(UpdateFDTransactionDto dto, string modifiedBy);
        Task<bool> DeleteFDTransactionAsync(int fdtransactionId, string modifiedBy);
        Task<FDTransactionResponseDto?> GetFDTransactionByIdAsync(int fdtransactionId);
        Task<IEnumerable<FDTransactionResponseDto>> GetAllFDTransactionsAsync();
    }
} 