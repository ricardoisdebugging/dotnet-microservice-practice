using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.PlatformDomain
{
    public interface IPlatformRepository
    {
        public Task<int> CreatePlatformAsync(Platform createdPlatform);
        public Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        public Task<Platform> GetPlatformById(int platformId);
        public Task<Platform> UpdatePlatformAsync(Platform updatedPlatform);
    }
}
