using AutoMapper;
using PlatformService.PlatformDomain;

namespace PlatformService.Utils
{
    public class PlatformMappingProfile: Profile
    {
        public PlatformMappingProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformWriteDto, Platform>();
        }
    }
}
