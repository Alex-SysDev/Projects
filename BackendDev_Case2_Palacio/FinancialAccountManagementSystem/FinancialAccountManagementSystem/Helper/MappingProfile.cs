using AutoMapper;
using FinancialAccountManagementSystem.Dto;
using FinancialAccountManagementSystem.Models;

namespace FinancialAccountManagementSystem.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountWriteDto>();
            CreateMap<AccountWriteDto, Account>();
            CreateMap<decimal, AccountsTotalDto>()
            .   ForMember(dest => dest.TotalBalance, opt => opt.MapFrom(src => src));

            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionCreateDto>();
            CreateMap<TransactionCreateDto, Transaction>();
        }       
    }
}
