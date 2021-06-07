using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }
        // get all books
        public List<Book> GetAllBooks()
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        // get a single book
        public Book GetBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(n => n.Id == bookId);
        }

        // add new book 
        public void AddBook(BookVM book) {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        // update book
        public Book UpdateBookById(int bookId, BookVM book) 
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId); // find book by id
            // update book
            if (_book != null) 
            {
             _book.Title = book.Title;
            _book.Description = book.Description;
            _book.IsRead = book.IsRead;
            _book.DateRead = book.DateRead;
            _book.Rate = book.Rate;
            _book.Genre = book.Genre;
            _book.Author = book.Author;
            _book.CoverUrl = book.CoverUrl;
            }
            _context.SaveChanges();
            return _book;
        }

        // delete book
        public void DeleteBookById(int bookId) 
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null) 
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
