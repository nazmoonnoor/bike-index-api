using AutoMapper;

namespace Swapfiets.Theft.Service.Mappers
{
    /// <summary>
    /// Maps models and domains classes
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Configure the map
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Core.Domains.City, Models.City>()
                .ReverseMap();
        }
    }
}
