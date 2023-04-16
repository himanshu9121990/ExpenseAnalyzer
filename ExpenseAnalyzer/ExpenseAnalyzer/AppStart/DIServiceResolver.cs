namespace AdGo.API.Document.AppStart
{
    /// <summary>
    /// Dependency injection resolver class.
    /// </summary>
    public static class DIServiceResolver
    {
        /// <summary>
        /// Configure service for Dependency Injection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<DocumentionConfiguration>(configuration.GetSection("DocumentationSettings"));
            //services.Configure<InternalApiConfiguration>(configuration.GetSection("InternalApiSettings"));
            //services.Configure<MessageConfiguration>(configuration.GetSection("MessageSettings"));
            //services.AddTransient<IHttpContextDetail, HttpContextDetail>();
            //services.AddTransient<IHttpClientHelper, HttpClientHelper>();
            //services.AddTransient<IDocumentRepository, DocumentRepository>();
            //services.AddScoped(typeof(IOmniSearch<>), typeof(OmniSearch<>));
            //services.AddTransient<ISubmissionRepository, SubmissionRepository>();
            //services.AddTransient<IMasterDataRepository, MasterDataRepository>();
            //services.AddTransient<ILiteMasterDataRepository, LiteMasterDataRepository>();
            //services.AddTransient<IQuoteRepository, QuoteRepository>();
            //services.AddTransient<IPartyRepository, PartyRepository>();
            //services.AddTransient<IDocumentTransformation, DocumentTransformation>();
            //services.AddTransient<IMessageRepository, MessageRepository>();
            //services.AddSingleton(typeof(MessageHelper));
        }
    }
}
