namespace EA.FeedProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FeedFileWatcher fileWatcher;

        public Worker(ILogger<Worker> logger, FeedFileWatcher fileWatcher)
        {
            _logger = logger;
            this.fileWatcher = fileWatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.fileWatcher.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}