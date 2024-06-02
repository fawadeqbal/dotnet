using TodoApi.Context;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Explicitly set the connection string and database name
var connectionString = "mongodb+srv://fawadeqbal00:fawad1@cluster0.zpil5a7.mongodb.net/";
var databaseName = "library";

// Add services to the container
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
