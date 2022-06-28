using Microsoft.Extensions.Configuration;
using PlatformService.PlatformDomain;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.Utils.CommandService
{
    public class CommandClient : ICommandClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CommandClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendMessageToCommand(PlatformReadDto platformReadDto)
        {
            var httpContext = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json");

            var apiPath = _configuration["CommandService"];
            var response = await _httpClient.PostAsync($"{apiPath}/api/c/platforms/", httpContext);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(">>>Sync Post to CommandService was Ok");
            }
            else
            {
                Console.WriteLine(">>>Sync Post to CommandService was NOT Ok");
            }
        }
    }
}
