using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BigDataService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<ApiConfiguration> _options;

        public Worker(ILogger<Worker> logger, IOptions<ApiConfiguration> options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_options.Value.ApiAddress) })
            {
                client.DefaultRequestHeaders.Add("X-Gravitee-Api-Key", _options.Value.ApiKey);
                DataSet? dataSet = await client.GetFromJsonAsync<DataSet>("collections/observation/items?period=latest-10-minutes");
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}