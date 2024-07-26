using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3.Models
{
    public interface IBookRepository
    {
        IQueryable<tblbook> GetQueryableBooks();
        List<tblbook> GetBooks();

        tblbook GetBookByID(int Bookid);
        void insert(tblbook book_);
        void update(tblbook book_);
        void delete(int Bookid);

        void savechanges();
    }
}
