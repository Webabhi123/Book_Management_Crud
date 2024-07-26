using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace assignment3.Models
{
    public partial class bookcontext : DbContext
    {
        public bookcontext()
            : base("name=bookcontext")
        {
        }

        public virtual DbSet<tblbook> tblbooks { get; set; }

        public IQueryable<tblbook> GetBooks()
        {
            return tblbooks.OrderByDescending(book => book.Bookid);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
