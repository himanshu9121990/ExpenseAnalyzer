namespace EA.FeedProcessor.Model
{
    public class WatcherConfiguration
    {
        public string SourceDirectory { get; set; } =  Path.Combine(AppContext.BaseDirectory,"Feed");
        public string DestinationDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "Feed-Processed");
    }
}
