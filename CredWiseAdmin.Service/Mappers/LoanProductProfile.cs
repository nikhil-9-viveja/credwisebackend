using AutoMapper;
using CredWiseAdmin.Core.DTOs.LoanProduct;
using CredWiseAdmin.Core.Entities;

namespace CredWiseAdmin.Service.Mappers
{
    public class LoanProductProfile : Profile
    {
        public LoanProductProfile()
        {
            CreateMap<LoanProduct, LoanProductResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LoanProductId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.LoanDetail, opt => opt.MapFrom(src => MapLoanDetail(src)));

            CreateMap<CreateLoanProductDto, LoanProduct>();
            CreateMap<UpdateLoanProductDto, LoanProduct>();
        }

        private object MapLoanDetail(LoanProduct product)
        {
            switch (product.LoanType.ToUpper())
            {
                case "HOME":
                    return product.HomeLoanDetail != null ? new HomeLoanDetailDto
                    {
                        InterestRate = product.HomeLoanDetail.InterestRate,
                        TenureMonths = product.HomeLoanDetail.TenureMonths,
                        ProcessingFee = product.HomeLoanDetail.ProcessingFee,
                        DownPaymentPercentage = product.HomeLoanDetail.DownPaymentPercentage,
                        RepaymentType = "EMI"
                    } : null;

                case "PERSONAL":
                    return product.PersonalLoanDetail != null ? new PersonalLoanDetailDto
                    {
                        InterestRate = product.PersonalLoanDetail.InterestRate,
                        TenureMonths = product.PersonalLoanDetail.TenureMonths,
                        ProcessingFee = product.PersonalLoanDetail.ProcessingFee,
                        MinSalaryRequired = product.PersonalLoanDetail.MinSalaryRequired,
                        RepaymentType = "EMI"
                    } : null;

                case "GOLD":
                    return product.GoldLoanDetail != null ? new GoldLoanDetailDto
                    {
                        InterestRate = product.GoldLoanDetail.InterestRate,
                        TenureMonths = product.GoldLoanDetail.TenureMonths,
                        ProcessingFee = product.GoldLoanDetail.ProcessingFee,
                        GoldPurityRequired = product.GoldLoanDetail.GoldPurityRequired,
                        RepaymentType = product.GoldLoanDetail.RepaymentType
                    } : null;

                default:
                    return null;
            }
        }
    }
} 