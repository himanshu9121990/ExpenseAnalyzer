using EA.FeedProcessor.Interface;
using EA.FeedProcessor.Model;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EA.FeedProcessor
{
    public class FeedFileWatcher : IDisposable
    {
        private FileSystemWatcher fileWatcher;
        private readonly IServiceProvider serviceProvider;

        public FeedFileWatcher(IOptions<WatcherConfiguration> configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            this.serviceProvider = serviceProvider;
            if (!Directory.Exists(this.Configuration.Value.SourceDirectory))
            {
                Directory.CreateDirectory(this.Configuration.Value.SourceDirectory);
            }
            if (!Directory.Exists(this.Configuration.Value.DestinationDirectory))
            {
                Directory.CreateDirectory(this.Configuration.Value.DestinationDirectory);
            }
        }
        public IOptions<WatcherConfiguration> Configuration { get; }

        public void Start()
        {
            fileWatcher = new FileSystemWatcher(this.Configuration.Value.SourceDirectory);
            fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

            fileWatcher.Created += OnCreated;

            fileWatcher.IncludeSubdirectories = false;
            fileWatcher.EnableRaisingEvents = true;

        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Debug.WriteLine(value);

            IFeedProcessor feedProcessor = serviceProvider.GetService<IFeedProcessor>()!;
            var result = await feedProcessor.Process(e.FullPath);

            if (result.Success)
            {
                feedProcessor.File.MoveTo(Path.Combine(Configuration.Value.DestinationDirectory, feedProcessor.File.Name));
            }
            Debug.WriteLine(result.Success);
        }

        public void Dispose()
        {
            this.fileWatcher.Dispose();
        }
    }
}
