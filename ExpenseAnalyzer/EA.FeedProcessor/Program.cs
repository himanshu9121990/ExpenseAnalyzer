using EA.FeedProcessor.Implementation;
using EA.FeedProcessor.Interface;
using EA.FeedProcessor.Model;
using EA.Repository;
using EA.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EA.FeedProcessor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<WatcherConfiguration>(hostContext.Configuration.GetSection("WatcherConfiguration"));
                    services.AddSingleton<FeedFileWatcher>();
                    services.AddDbContextPool<DataContext>(opt => {
                        opt.UseJet(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                    });

                    //services.AddScoped<IDbContextFactory, DbContextFactory>();
                    services.AddTransient<IHdfcSbStatementRepository, HdfcSbStatementRepository>();
                    services.AddTransient<IFeedProcessor, HdfcSbFileProcessor>();
                    services.AddHostedService<Worker>();

                })
                .Build();

            host.Run();
        }
    }
}