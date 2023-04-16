using AdGo.API.Common.Model;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace AdGo.API.Document.AppStart
{
    /// <summary>
    /// Configure api versioning setup.
    /// </summary>
    public class ApiVersion
    {
        /// <summary>
        /// Configure service for Dependency Injection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            ApiVersionConfiguration apiVersionConfig = configuration.GetSection("ApiVersionConfiguration").Get<ApiVersionConfiguration>() ?? new ApiVersionConfiguration();

            List<IApiVersionReader> apiVersionReaders = new List<IApiVersionReader>();
            if (apiVersionConfig.EnableUrlBaseApiVerioning)
            {
                apiVersionReaders.Add(new UrlSegmentApiVersionReader());
            }

            if (apiVersionConfig.EnableHeaderBaseApiVerioning)
            {
                apiVersionReaders.Add(new HeaderApiVersionReader(apiVersionConfig.ApiHeaderName));
            }

            if (apiVersionConfig.EnableMediaTypeBaseApiVersioning)
            {
                apiVersionReaders.Add(new MediaTypeApiVersionReader(apiVersionConfig.ApiHeaderName));
            }

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(apiVersionConfig.MajorVersion, apiVersionConfig.MinorVersion);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(apiVersionReaders);
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerOptions>();
        }
    }
}
