using my_book.Data.ViewModels;
using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
	public class PublisherServices
	{
		private AppDbContext _context;
		public PublisherServices(AppDbContext context)
		{
			_context = context;
		}

		// get publisher with books and authors list
		public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId) 
		{
			var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
				.Select(n => new PublisherWithBooksAndAuthorsVM()
				{
					Name = n.FullName,
					BookAuthors = n.Books.Select(n => new BookAuthorVM()
					{
						BookName = n.Title,
						BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
					}).ToList()
				}).FirstOrDefault();
			return _publisherData;
		}

		// add publishers
		public Publisher AddPublisher(PublisherVM publisher) 
		{
			var _publisher = new Publisher()
			{
				FullName = publisher.FullName
			};

			_context.Publishers.Add(_publisher);
			_context.SaveChanges();

			return _publisher;
		}

		// get publisher by id
		public Publisher GetPublisherById(int id) 
		{
			var publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
			return publisher;
		}

		public void DeletePublisherById(int id)
		{
			var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
			if (_publisher != null) 
			{
				_context.Publishers.Remove(_publisher);
				_context.SaveChanges();
			}
		}
	}
}
