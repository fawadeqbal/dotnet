using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Context;
using TodoApi.Models;
using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var mongoDbSettings = configuration.GetSection("MongoDbSettings");
var connectionString = mongoDbSettings["ConnectionStrings"];
var databaseName = mongoDbSettings["DatabaseName"];

builder.Services.AddSingleton(sp => new MongoDbContext(connectionString, databaseName));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

app.MapGet("/", () => new { Message = "Welcome to Book API!" });

app.MapPost("/addBook", (Book book, IBookService bookService) =>
{
    var res = bookService.AddBook(book);
    return Results.Ok(res);
});

app.MapGet("/getAllBooks", (IBookService bookService) =>
{
    var books = bookService.GetAllBooks();
    return Results.Ok(books);
});

app.MapGet("/getBookById/{id}", (string id, IBookRepository bookRepository) =>
{
    var book = bookRepository.GetBookById(id);
    if (book != null)
    {
        return Results.Ok(book);
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();