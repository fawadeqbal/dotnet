using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IBookService
    {
        ApiResponse AddBook(Book book);
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(string id);
    }

   
}
