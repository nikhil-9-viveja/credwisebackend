using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;
using CredWiseAdmin.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredWiseAdmin.Service.Interfaces;
using System;
using CredWiseAdmin.Service.Exceptions;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CredWiseAdmin.Service
{
    public class FDApplicationService : IFDApplicationService
    {
        private readonly IFDApplicationRepository _fdApplicationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFDTypeRepository _fdTypeRepository;
        private readonly IMapper _mapper;

        public FDApplicationService(IFDApplicationRepository fdApplicationRepository, IUserRepository userRepository, IFDTypeRepository fdTypeRepository, IMapper mapper)
        {
            _fdApplicationRepository = fdApplicationRepository;
            _userRepository = userRepository;
            _fdTypeRepository = fdTypeRepository;
            _mapper = mapper;
        }

        public async Task<FDApplicationResponseDto> CreateFDApplicationAsync(CreateFDApplicationDto dto, string createdBy)
        {
            // 1. Lookup user by email
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new NotFoundException("User not found");

            // 2. Lookup FDType by ID
            var fdType = await _fdTypeRepository.GetByIdAsync(dto.FDTypeId);
            if (fdType == null)
                throw new NotFoundException("FD Type not found");

            // 3. Validate amount
            if (dto.Amount < fdType.MinAmount || dto.Amount > fdType.MaxAmount)
                throw new ValidationException("Amount out of allowed range");

            // 4. Create FDApplication
            var now = DateTime.UtcNow;
            var fdApp = new Fdapplication
            {
                UserId = user.UserId,
                FdtypeId = fdType.FdtypeId,
                Amount = dto.Amount,
                InterestRate = fdType.InterestRate,
                Duration = fdType.Duration,
                Status = "Active",
                CreatedBy = createdBy,
                CreatedAt = now,
                ModifiedBy = createdBy,
                ModifiedAt = now,
                IsActive = true,
                MaturityDate = now.AddMonths(fdType.Duration),
                MaturityAmount = dto.Amount + (dto.Amount * fdType.InterestRate * fdType.Duration / 12 / 100)
            };

            try
            {
                var result = await _fdApplicationRepository.AddAsync(fdApp);
                return _mapper.Map<FDApplicationResponseDto>(result);
            }
            catch (Exception ex)
            {
                // Print to console (will show in Output window if running in Debug mode)
                Console.WriteLine("EXCEPTION: " + ex.ToString());
                if (ex.InnerException != null)
                    Console.WriteLine("INNER EXCEPTION: " + ex.InnerException.Message);

                // Optionally, write to a file for guaranteed visibility
                System.IO.File.WriteAllText(@"C:\temp\fdapplication_error.txt", ex.ToString() + "\n" + (ex.InnerException?.Message ?? ""));

                throw; // rethrow so you still see the error in Swagger
            }
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
            return _mapper.Map<FDApplicationResponseDto>(result);
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
            return fdApplication != null ? _mapper.Map<FDApplicationResponseDto>(fdApplication) : null;
        }

        public async Task<IEnumerable<FDApplicationResponseDto>> GetAllFDApplicationsAsync()
        {
            var fdApplications = await _fdApplicationRepository.GetAllAsync();
            return fdApplications.Select(fdApplication => _mapper.Map<FDApplicationResponseDto>(fdApplication));
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
                ModifiedBy = fdApplication.ModifiedBy,
                CreatedAt = fdApplication.CreatedAt,
                ModifiedAt = fdApplication.ModifiedAt,
                MaturityDate = fdApplication.MaturityDate,
                MaturityAmount = fdApplication.MaturityAmount,
                FDTypeName = fdApplication.Fdtype?.Name
            };
        }
    }
} 