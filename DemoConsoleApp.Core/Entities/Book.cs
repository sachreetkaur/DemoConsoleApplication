using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Core.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublication { get; set; }
        public DateTime BookDate { get; set; }
        public long BookPrice { get; set; }
        public long BookQuantity { get; set; }
    }
}
