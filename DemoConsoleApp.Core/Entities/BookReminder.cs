using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Core.Entities
{
    public class BookReminder
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public int DaysExceededToReturnBook { get; set; }
        public string MessageText { get; set; }
        public bool IsMessageSent { get; set; } = false;
    }
}
