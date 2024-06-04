using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TodoApi.Context;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(MongoDbContext context)
        {
            _books = context.Books;
        }

        public void AddBook(Book book)
        {
            _books.InsertOne(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.Find(book => true).ToList();
        }

        public Book GetBookById(string id)
        {
            return _books.Find(book => book._id.ToString() == id).FirstOrDefault();
        }   

    }
}
