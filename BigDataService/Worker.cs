using BigDataService.Classes;
using DAL;
using DAL.Entities;
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
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IOptions<ApiConfiguration> options, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Stopwatch watch = new Stopwatch();
            bool start = true;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (watch.Elapsed.TotalMinutes >= 10 || start)
                {
                    start = false;
                    watch.Restart();
                    try
                    {
                        await MapAndLoad();
                        _logger.LogInformation("Data set loaded successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }
            await Task.Delay(1000, stoppingToken);
        }

        public async Task MapAndLoad()
        {
            JsonDataSet? dataSet;
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(_options.Value.ApiAddress) })
            {
                client.DefaultRequestHeaders.Add("X-Gravitee-Api-Key", _options.Value.ApiKey);
                dataSet = await client.GetFromJsonAsync<JsonDataSet>("collections/observation/items?period=latest-10-minutes");
            }
            if (dataSet != null)
            {
                if (dataSet.Features != null && dataSet.Features.Any())
                {
                    using (BigDataContext context = _serviceProvider.CreateScope().ServiceProvider.GetService<BigDataContext>())
                    {

                        await context.DataSets.AddAsync(new()
                        {
                            Type = dataSet.Type,
                            Features = dataSet.Features.Select(x => new Feature
                            {
                                Type = x.Type,
                                Id = x.Id,
                                Geometry = new Geometry
                                {
                                    Coordinates = x.Geometry.Coordinates.Select(x => new Coordinate { Value = x }).ToList(),
                                    Type = x.Geometry.Type
                                },
                                Properties = new Properties
                                {
                                    Created = x.Properties.Created,
                                    Observed = x.Properties.Observed,
                                    ParameterId = x.Properties.ParameterId,
                                    StationId = x.Properties.StationId,
                                    Value = x.Properties.Value
                                }
                            }).ToList()
                        });
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogCritical("Api call returns empty in features");
                }
            }
            else
            {
                _logger.LogCritical("Api called returned nothing");
            }
        }
    }
}