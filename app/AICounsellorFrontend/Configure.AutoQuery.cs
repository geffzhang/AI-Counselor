using ServiceStack;
using ServiceStack.Data;

[assembly: HostingStartup(typeof(AICounsellorFrontend.ConfigureAutoQuery))]

namespace AICounsellorFrontend;

public class ConfigureAutoQuery : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Enable Audit History
            services.AddSingleton<ICrudEvents>(c =>
                new OrmLiteCrudEvents(c.GetService<IDbConnectionFactory>()));
        })
        .ConfigureAppHost(appHost => {

            // For TodosService
            appHost.Plugins.Add(new AutoQueryDataFeature());

            // For Bookings https://docs.servicestack.net/autoquery-crud-bookings
            appHost.Plugins.Add(new AutoQueryFeature
            {
                MaxLimit = 1000,
                //IncludeTotal = true,
            });

            appHost.Resolve<ICrudEvents>().InitSchema();
        });
}