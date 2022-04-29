using Microsoft.EntityFrameworkCore;
using PlatformService.PlatformDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _context;
        public PlatformRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<int> CreatePlatformAsync(Platform createdPlatform)
        {
            _context.Platforms.Add(createdPlatform);
            _ = await this.SaveChnagesAsync();
            return createdPlatform.PlatformId;
        }

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _context.Platforms.AsNoTracking().ToListAsync();
        }

        public Task<Platform> GetPlatformById(int platformId)
        {
            return _context.Platforms
                .Where(platform => platform.PlatformId == platformId)
                .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Platform> UpdatePlatformAsync(Platform updatedPlatform)
        {
            var currentPlatform = _context.Platforms
                .First(platform => platform.PlatformId == platform.PlatformId);
            if(currentPlatform is null)
            {
                throw new Exception($"Platform with Id {updatedPlatform.PlatformId} does not exist");
            }
            _context.Platforms.Update(updatedPlatform);
            _ = await this.SaveChnagesAsync();
            return updatedPlatform;
        }

        private async Task<bool> SaveChnagesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
