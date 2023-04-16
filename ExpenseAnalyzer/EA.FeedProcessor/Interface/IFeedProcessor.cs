using EA.FeedProcessor.Model;

namespace EA.FeedProcessor.Interface
{
    public interface IFeedProcessor
    {
        public FileInfo File { get; }

        Task<FeedProcess> Process(string FileFullname);
    }
}
