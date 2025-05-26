using AutoMapper;
using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Core.Mappings
{
    public class FDTypeMappingProfile : Profile
    {
        public FDTypeMappingProfile()
        {
            CreateMap<Fdtype, FDTypeResponseDto>();
            CreateMap<CreateFDTypeDto, Fdtype>();
            CreateMap<UpdateFDTypeDto, Fdtype>();
        }
    }
} 