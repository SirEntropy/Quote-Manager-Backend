using AutoMapper;
using QuoteManagement.Dtos;
using QuoteManager.Dtos;
using QuoteModels;

namespace QuoteManagement
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Quote, QuoteDto>();
            CreateMap<QuoteDto, Quote>().ForMember(dest=>dest.QuoteID,opt=>opt.Ignore());
            CreateMap<user, userDto>();
            CreateMap<userDto, user>();
        }
    }

    
}