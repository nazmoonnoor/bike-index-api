using AutoMapper;

namespace Swapfiets.Theft.Api.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Domains.City, Service.Models.City>()
                .ReverseMap();
        }
    }
}
