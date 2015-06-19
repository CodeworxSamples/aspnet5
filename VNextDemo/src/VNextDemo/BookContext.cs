using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using VNextDemo.Model;

namespace VNextDemo
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books
        {
            get;
            set;
        }
    }
}
