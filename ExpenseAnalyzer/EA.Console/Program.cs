using EA.Model;
using EA.Repository;
using Microsoft.Extensions.Configuration;

namespace EA.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello, World!");

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var fileNameOrConnectionString = config.GetConnectionString("DefaultConnection");

            using (DataContext dbContext = new DataContext(fileNameOrConnectionString))
            {
                foreach (Tag c in dbContext.Tags)
                {
                    System.Console.WriteLine(c.Name);
                }

            }
        }
    }
}