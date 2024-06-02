using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(string id);
    }
}
