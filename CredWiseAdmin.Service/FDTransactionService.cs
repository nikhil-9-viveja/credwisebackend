using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;
using System;
using System.Linq;
using AutoMapper;

namespace CredWiseAdmin.Service
{
    public class FDTransactionService : IFDTransactionService
    {
        private readonly IFDTransactionRepository _fdTransactionRepository;
        private readonly IMapper _mapper;

        public FDTransactionService(IFDTransactionRepository fdTransactionRepository, IMapper mapper)
        {
            _fdTransactionRepository = fdTransactionRepository;
            _mapper = mapper;
        }

        public async Task<FDTransactionResponseDto> CreateFDTransactionAsync(CreateFDTransactionDto dto, string createdBy)
        {
            var now = DateTime.UtcNow;
            var fdTransaction = new Fdtransaction
            {
                FdapplicationId = dto.FdapplicationId,
                TransactionType = dto.TransactionType,
                Amount = dto.Amount,
                TransactionDate = now,
                PaymentMethod = dto.PaymentMethod,
                TransactionStatus = "Success",
                IsActive = true,
                CreatedBy = string.IsNullOrWhiteSpace(createdBy) ? "system" : createdBy,
                CreatedAt = now,
                ModifiedBy = string.IsNullOrWhiteSpace(createdBy) ? "system" : createdBy,
                ModifiedAt = now,
                TransactionReference = $"FDTRX-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}"
            };

            try
            {
                Console.WriteLine($"ModifiedAt: {fdTransaction.ModifiedAt}");
                Console.WriteLine($"ModifiedBy: {fdTransaction.ModifiedBy}");
                var result = await _fdTransactionRepository.AddAsync(fdTransaction);
                return _mapper.Map<FDTransactionResponseDto>(result);
            }
            catch (Exception ex)
            {
                // Print to console (will show in Output window if running in Debug mode)
                Console.WriteLine("EXCEPTION: " + ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("INNER EXCEPTION: " + ex.InnerException.Message);

                // Optionally, write to a file for guaranteed visibility
                System.IO.File.WriteAllText(@"C:\temp\fdtransaction_error.txt", ex.ToString() + "\n" + (ex.InnerException?.Message ?? ""));

                throw; // rethrow so you still see the error in Swagger
            }
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
            fdTransaction.ModifiedAt = DateTime.UtcNow;

            var result = await _fdTransactionRepository.UpdateAsync(fdTransaction);
            return _mapper.Map<FDTransactionResponseDto>(result);
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
            return fdTransaction != null ? _mapper.Map<FDTransactionResponseDto>(fdTransaction) : null;
        }

        public async Task<IEnumerable<FDTransactionResponseDto>> GetAllFDTransactionsAsync()
        {
            var fdTransactions = await _fdTransactionRepository.GetAllAsync();
            return fdTransactions.Select(_mapper.Map<FDTransactionResponseDto>);
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
                TransactionReference = fdTransaction.TransactionReference,
                IsActive = fdTransaction.IsActive,
                CreatedAt = fdTransaction.CreatedAt,
                CreatedBy = fdTransaction.CreatedBy,
                ModifiedAt = fdTransaction.ModifiedAt,
                ModifiedBy = fdTransaction.ModifiedBy
            };
        }
    }
} 