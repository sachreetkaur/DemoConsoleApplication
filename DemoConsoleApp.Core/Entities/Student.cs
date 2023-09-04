using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Core.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string StudentDepartment { get; set; }
        public string StudentSemester { get; set; }
        public long StudentContact { get; set; }
        public string StudentEmail { get; set; }
    }
}
