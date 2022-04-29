using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.PlatformDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlatformRepository _platformRepository;

        public PlatformController(
            IPlatformRepository platformRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _platformRepository = platformRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllPlatformsAsync()
        {
            Console.WriteLine(">>>Getting Platforms...");
            var platforms = await _platformRepository.GetAllPlatformsAsync();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }


        [HttpGet("{platformId}", Name = "GetPlatformByIdAsync")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformByIdAsync([FromRoute] int platformId)
        {
            if(platformId <= 0)
            {
                return this.BadRequest(new
                    {
                        Message = "Platform Id should be greater than 0."
                    });
            }

            Console.WriteLine(">>>Getting target Platform...");
            var platform = await _platformRepository.GetPlatformById(platformId);
            if(!(platform is null))
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatformAsync(
            [FromBody] PlatformWriteDto platformWriteDto)
        {
            if(platformWriteDto is null)
            {
                return this.BadRequest(new
                {
                    Message = "Platform data should not be bull."
                });
            }

            Console.WriteLine(">>>Creating target Platform...");
            var platform = _mapper.Map<Platform>(platformWriteDto);
            _ = await _platformRepository.CreatePlatformAsync(platform);

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
            return CreatedAtRoute(
                nameof(GetPlatformByIdAsync),
                new { PlatformId = platform.PlatformId },
                platformReadDto);
        }

        [HttpPut]
        public async Task<ActionResult<PlatformReadDto>> UpdatePlatformAsync(
            [FromBody] PlatformWriteDto platformWriteDto)
        {
            if (platformWriteDto is null)
            {
                return this.BadRequest(new
                {
                    Message = "Platform data should not be bull."
                });
            }

            if (platformWriteDto.PlatformId <= 0)
            {
                return this.BadRequest(new
                {
                    Message = "Platform id should greater than 0."
                });
            }


            Console.WriteLine(">>>Update target Platform...");
            var platform = _mapper.Map<Platform>(platformWriteDto);
            _ = await _platformRepository.UpdatePlatformAsync(platform);
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }
    }
}
