using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Core.Entities
{
    public class IssueBook
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string StudentDepartment { get; set; }
        public string StudentSemester { get; set; }
        public long StudentContact { get; set; }
        public string StudentEmail { get; set; }
        public string BookName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
