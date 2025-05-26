//using AutoMapper;
//using CredWiseAdmin.Core.DTOs.LoanProduct;
//using CredWiseAdmin.Core.Entities;

//namespace CredWiseAdmin.Core.Mappings
//{
//    public class LoanProductMappingProfile : Profile
//    {
//        public LoanProductMappingProfile()
//        {
//            // Entity to Response DTO mappings
//            CreateMap<LoanProduct, LoanProductResponseDto>();
//            CreateMap<LoanProduct, LoanProductDetailResponseDto>()
//                .ForMember(dest => dest.RequiredDocuments, opt => opt.MapFrom(src => src.LoanProductDocuments));
//            CreateMap<LoanProductDocument, LoanProductDocumentResponseDto>();

//            // For create/update responses
//            CreateMap<LoanProduct, CreateLoanProductResponseDto>();
//            CreateMap<LoanProduct, UpdateLoanProductResponseDto>();

//            // DTO to Entity mappings
//            CreateMap<CreateLoanProductDto, LoanProduct>()
//                .ForMember(dest => dest.LoanProductId, opt => opt.Ignore())
//                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
//                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
//                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
//                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
//                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

//            CreateMap<UpdateLoanProductDto, LoanProduct>()
//                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
//                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
//                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
//                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
//        }
//    }
//}

using AutoMapper;
using CredWiseAdmin.Core.DTOs.LoanProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Core.Mappings
{
    public class LoanProductMappingProfile : Profile
    {
        public LoanProductMappingProfile()
        {
            // Entity to Response DTO mappings
            CreateMap<LoanProduct, LoanProductResponseDto>();
            CreateMap<LoanProduct, LoanProductDetailResponseDto>()
                .ForMember(dest => dest.RequiredDocuments, opt => opt.MapFrom(src => src.LoanProductDocuments));
            CreateMap<LoanProductDocument, LoanProductDocumentResponseDto>();

            // For create/update responses
            //CreateMap<LoanProduct, CreateLoanProductResponseDto>();
            CreateMap<LoanProduct, UpdateLoanProductResponseDto>();

            // DTO to Entity mappings
            CreateMap<CreateLoanProductDto, LoanProduct>()
                .ForMember(dest => dest.LoanProductId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateLoanProductDto, LoanProduct>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}