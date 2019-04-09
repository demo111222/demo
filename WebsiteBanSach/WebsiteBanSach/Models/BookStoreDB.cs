using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
    public class BookStoreDB : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookStoreDB() : base("DefaultConnection")
        {

        }
    }
}