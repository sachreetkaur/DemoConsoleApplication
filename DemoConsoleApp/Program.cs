using Microsoft.Extensions.DependencyInjection;
using DemoConsoleApp.StructureMap;
using StructureMap;
using RabbitMQ.Client;
using System.Text;

namespace DemoConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection()
                .AddLogging();


            var container = new Container();
            container.Configure(config =>
            {
                config.AddRegistry(new ApplicationRegistry());

                config.Populate(services);
            });


            var serviceProvider = container.GetInstance<IServiceProvider>();
            var bookReminderService = container.GetInstance<BookReminderService>();
            bookReminderService.CheckBooksToNotify();


        }
        
    }
}

