using PlatformService.PlatformDomain;
using System.Threading.Tasks;

namespace PlatformService.Utils.CommandService
{
    public interface ICommandClient
    {
        Task SendMessageToCommand(PlatformReadDto platformReadDto);
    }
}
