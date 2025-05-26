using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;
using System;
using System.Linq;

namespace CredWiseAdmin.Service
{
    public class FDTransactionService : IFDTransactionService
    {
        private readonly IFDTransactionRepository _fdTransactionRepository;

        public FDTransactionService(IFDTransactionRepository fdTransactionRepository)
        {
            _fdTransactionRepository = fdTransactionRepository;
        }

        public async Task<FDTransactionResponseDto> CreateFDTransactionAsync(CreateFDTransactionDto dto, string createdBy)
        {
            var fdTransaction = new Fdtransaction
            {
                FdapplicationId = dto.FdapplicationId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                TransactionStatus = "Pending",
                IsActive = true,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                TransactionReference = Guid.NewGuid().ToString().Substring(0, 12)
            };

            var result = await _fdTransactionRepository.AddAsync(fdTransaction);
            return MapToResponseDto(result);
        }

        public async Task<FDTransactionResponseDto> UpdateFDTransactionAsync(UpdateFDTransactionDto dto, string modifiedBy)
        {
            var fdTransaction = await _fdTransactionRepository.GetByIdAsync(dto.FdtransactionId);
            if (fdTransaction == null)
                return null;

            fdTransaction.FdapplicationId = dto.FdapplicationId;
            fdTransaction.TransactionType = dto.TransactionType;
            fdTransaction.Amount = dto.Amount;
            fdTransaction.TransactionDate = dto.TransactionDate;
            fdTransaction.PaymentMethod = dto.PaymentMethod;
            fdTransaction.TransactionStatus = dto.TransactionStatus;
            fdTransaction.ModifiedBy = modifiedBy;

            var result = await _fdTransactionRepository.UpdateAsync(fdTransaction);
            return MapToResponseDto(result);
        }

        public async Task<bool> DeleteFDTransactionAsync(int fdtransactionId, string modifiedBy)
        {
            var fdTransaction = await _fdTransactionRepository.GetByIdAsync(fdtransactionId);
            if (fdTransaction == null)
                return false;

            await _fdTransactionRepository.DeleteAsync(fdtransactionId);
            return true;
        }

        public async Task<FDTransactionResponseDto?> GetFDTransactionByIdAsync(int fdtransactionId)
        {
            var fdTransaction = await _fdTransactionRepository.GetByIdAsync(fdtransactionId);
            return fdTransaction != null ? MapToResponseDto(fdTransaction) : null;
        }

        public async Task<IEnumerable<FDTransactionResponseDto>> GetAllFDTransactionsAsync()
        {
            var fdTransactions = await _fdTransactionRepository.GetAllAsync();
            return fdTransactions.Select(MapToResponseDto);
        }

        private FDTransactionResponseDto MapToResponseDto(Fdtransaction fdTransaction)
        {
            return new FDTransactionResponseDto
            {
                FdtransactionId = fdTransaction.FdtransactionId,
                FdapplicationId = fdTransaction.FdapplicationId,
                TransactionType = fdTransaction.TransactionType,
                Amount = fdTransaction.Amount,
                TransactionDate = fdTransaction.TransactionDate,
                PaymentMethod = fdTransaction.PaymentMethod,
                TransactionStatus = fdTransaction.TransactionStatus,
                IsActive = fdTransaction.IsActive,
                CreatedBy = fdTransaction.CreatedBy,
                ModifiedBy = fdTransaction.ModifiedBy
            };
        }
    }
} 