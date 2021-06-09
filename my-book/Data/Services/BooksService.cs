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
        public BookWithAuthorsVM GetBookById(int bookId)
        {
            // return _context.Books.FirstOrDefault(n => n.Id == bookId);
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {
              Title = book.Title,
              Discription = book.Description,
              IsRead = book.IsRead,
              DateRead =book.IsRead ? book.DateRead.Value: null,
              Rate = book.IsRead ? book.Rate.Value:null,
              Genre = book.Genre,
              CoverUrl = book.CoverUrl,
              PublisherName = book.Publisher.FullName,
              AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList(),

            }).FirstOrDefault();
            return _bookWithAuthors;
        }

        // add new book with authors
        public void AddBookWithAuthors(BookVM book) {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
               // Author = book.Author, doesn't need it because of the relationship
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

			// adding data book authors table
			foreach (var id in book.AuthorIds)
			{
                var _book_authors = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                _context.Books_Authors.Add(_book_authors);
                _context.SaveChanges();
			}
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
           // _book.Author = book.Author;
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
