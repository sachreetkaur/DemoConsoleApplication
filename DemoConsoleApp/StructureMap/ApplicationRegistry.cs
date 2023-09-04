using DemoConsoleApp.DBconnect.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.StructureMap
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory
                   (assembly => assembly.GetName().Name.StartsWith("DemoConsoleApp."));
                scanner.AssemblyContainingType(typeof(Program));
                scanner.WithDefaultConventions();


            });

            var configurationBuilder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("DBConnectionString");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DemoServiceContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);


            string path = configuration["AppLogPath"];

            var logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(configuration)
                    .WriteTo.File(path + @"\Logs\log-{Date}.txt")
                  .CreateLogger();

            Log.Logger = logger;


            For<ILogger>().Use(logger);
            For<IConfiguration>().Use(configuration).Singleton();

        }
    }
}
