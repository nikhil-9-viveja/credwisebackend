using AutoMapper;
using CredWiseAdmin.Core.DTOs.FDProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Core.Mappings
{
    public class FDApplicationMappingProfile : Profile
    {
        public FDApplicationMappingProfile()
        {
            CreateMap<Fdapplication, FDApplicationResponseDto>();
            CreateMap<CreateFDApplicationDto, Fdapplication>();
            CreateMap<UpdateFDApplicationDto, Fdapplication>();
        }
    }
} 