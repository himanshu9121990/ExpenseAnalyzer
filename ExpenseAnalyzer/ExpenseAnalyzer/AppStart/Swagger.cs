using AdGo.API.Common.Model;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AdGo.API.Document.AppStart
{
    /// <summary>
    /// 
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.DescribeAllParametersInCamelCase();

                // Bearer token authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Client access key for authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = configuration["AccessKeyHeaderName"],
                });

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                };

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });

                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var xmlFileCommon = $"{Assembly.GetAssembly(typeof(ResponseModelBase)).GetName().Name}.xml";
                var xmlPathCommon = Path.Combine(AppContext.BaseDirectory, xmlFileCommon);
                c.IncludeXmlComments(xmlPathCommon);

                c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date" });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Enable(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(configuration["SwaggerV1EndPoint"], "API V1");
            });
        }
    }
}
