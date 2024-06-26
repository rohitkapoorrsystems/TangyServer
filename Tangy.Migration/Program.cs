using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Notification.Migration;

var host = Host.CreateDefaultBuilder()
               .ConfigureServices(services =>
               {
                   services.AddDbContext<MigrationDbContext>(options => options.EnableDetailedErrors());
               })
               .Build();

await host.RunAsync();
