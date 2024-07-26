using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assignment3.Models
{
    public class BookRepository:IBookRepository
    {
        private readonly bookcontext _context;

        public BookRepository(bookcontext context)
        {
            _context = context;
        }
        public IQueryable<tblbook> GetQueryableBooks()
        {
            return _context.GetBooks();
        }
        public List<tblbook> GetBooks()
        {
            return _context.tblbooks.ToList();
        }
        public tblbook GetBookByID(int Bookid)
        {
            return _context.tblbooks.Find(Bookid);
        }

        public void insert(tblbook book_)
        {
            _context.tblbooks.Add(book_);
        }
        public void update(tblbook book_) 
        {
            _context.Entry(book_).State = System.Data.Entity.EntityState.Modified;
        }
        public void delete(int Bookid)
        {
            tblbook book_ = _context.tblbooks.Find(Bookid);
            _context.tblbooks.Remove(book_);
        }
        public void savechanges()
        {
            _context.SaveChanges();
        }

    }
}