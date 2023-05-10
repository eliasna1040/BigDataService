using BigDataService;
using DAL;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<ApiConfiguration>(configuration.GetSection(nameof(ApiConfiguration)));
        services.AddHostedService<Worker>();
        DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlServer(hostContext.Configuration.GetConnectionString("BigData"));
        services.AddDbContext<BigDataContext>(x => x.UseSqlServer(hostContext.Configuration.GetConnectionString("BigData")));
    })
    .Build();
host.Run();
