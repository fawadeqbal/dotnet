using System;
using System.Collections.Generic;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ApiResponse AddBook(Book book)
        {
            try
            {
                _bookRepository.AddBook(book);
                return new ApiResponse { Success = true, Message = "Book added successfully" };
            }
            catch (Exception ex)
            {
                if (IsDuplicateKeyError(ex))
                {
                    // Handle duplicate key error
                    return new ApiResponse { Success = false, Message = "A book with the same ID already exists" };
                }
                else
                {
                    // Handle other types of errors
                    return new ApiResponse { Success = false, Message = "An error occurred while adding the book" };
                }
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }
        public Book GetBookById(String id){
            return _bookRepository.GetBookById(id);
        }

        private bool IsDuplicateKeyError(Exception ex)
        {
            // Check if the exception message contains the MongoDB duplicate key error code
            return ex.Message.Contains("E11000");
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
