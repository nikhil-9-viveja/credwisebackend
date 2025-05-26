using AutoMapper;
using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Core.Mappings
{
    public class FDTransactionMappingProfile : Profile
    {
        public FDTransactionMappingProfile()
        {
            CreateMap<Fdtransaction, FDTransactionResponseDto>();
            CreateMap<CreateFDTransactionDto, Fdtransaction>();
            CreateMap<UpdateFDTransactionDto, Fdtransaction>();
        }
    }
} 