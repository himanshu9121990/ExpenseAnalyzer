using EA.Model;
using EA.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceProvider services;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceProvider services)
        {
            _logger = logger;
            this.services = services;
        }

        [HttpGet(Name = "GetTags")]
        public List<Tag> GetTags()
        {
            List<Tag> result = null;
            using (var scope = services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetService<DataContext>();
                result = dataContext?.Tags.ToList()!;
            }
            return result;
        }

        [HttpGet(Name = "GetHDFCTransaction")]
        public List<HdfcSbStatement> GetHDFCTransaction()
        {
            List<HdfcSbStatement> result = null;
            using (var scope = services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetService<DataContext>();
                result = dataContext?.HdfcSbStatements.Take(10).ToList()!;
            }
            return result;
        }
    }
}