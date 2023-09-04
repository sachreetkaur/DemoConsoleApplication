using DemoConsoleApp.Core.Entities;
using DemoConsoleApp.DBconnect.Data;
using DemoConsoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly DemoServiceContext _dbContext;
        public BookService(DemoServiceContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<IssueBook> GetBooksIssued()
        {

            List<IssueBook> booklist = _dbContext.IssueBook.Where(issueBook => issueBook.ReturnDate == null).ToList();
            return booklist;
        }


        public void SendMsgToStudentForBookReminder(int studentId, int bookId, int daysExceeded, string messageText)
        {

            bool reminderExists = _dbContext.Set<BookReminder>()
             .Any(x => x.StudentId == studentId && x.BookId == bookId);

            if (!reminderExists)
            {
                var reminderObj = new BookReminder
                {
                    BookId = bookId,
                    DaysExceededToReturnBook = daysExceeded,
                    MessageText = messageText,
                    StudentId = studentId,
                };

                _dbContext.Set<BookReminder>().Add(reminderObj);
                _dbContext.SaveChanges();

            }
        }
    }
}
