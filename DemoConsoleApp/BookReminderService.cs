using DemoConsoleApp.Services.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp
{
    public class BookReminderService
    {
        private readonly IBookService _bookService;
        private readonly IConfiguration _configuration;
        private readonly int _days;

         

        public BookReminderService(IBookService bookService, IConfiguration configuration)
        {
            this._bookService = bookService;
            this._configuration = configuration;
            this._days = Convert.ToInt32(configuration["DaysBookIssuedLimit"]);
        }

        public void CheckBooksToNotify()
        {
            var booksToNotify = _bookService.GetBooksIssued();

            foreach (var book in booksToNotify)
            {
                var currentDate = DateTime.Now.Date;
                int daysExceeded = (currentDate - book.IssueDate).Days;

                if (daysExceeded > _days)
                {
                    int daysExceededToReturn = daysExceeded - _days;
                    string message = $"{daysExceededToReturn} days exceeded to return book {book.BookName} for student with studentId {book.StudentId}";
                    Console.WriteLine(message);
                    Log.Information(message);
                    var integrationEventData = JsonConvert.SerializeObject(new
                    {
                        StudentId = book.StudentId,
                        Message = message
                    });
                    PublishToMessageQueue("reminder.add", integrationEventData);

                }

            }
        }
        public void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            using (var connection = factory.CreateConnection())
            {

                using (var channel = connection.CreateModel())
                {

                    var body = Encoding.UTF8.GetBytes(eventData);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(
                        exchange: "library",
                        routingKey: integrationEvent,
                        basicProperties: properties,
                        body: body);

                }
            }
        }

    }
}
