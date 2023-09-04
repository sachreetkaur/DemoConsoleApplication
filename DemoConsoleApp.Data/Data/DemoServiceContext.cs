using DemoConsoleApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.DBconnect.Data
{
    public class DemoServiceContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configure your database provider here
                optionsBuilder.UseSqlServer("Server= DESKTOP-PG32PRU\\SQLEXPRESS; Initial Catalog = LMS; Trusted_Connection = SSPI; Encrypt = false; Integrated Security = True;");
            }
        }
        public DemoServiceContext(DbContextOptions<DemoServiceContext> options)
        : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public DbSet<IssueBook> IssueBook { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<BookReminder> BookReminder { get; set; }
    }
}
