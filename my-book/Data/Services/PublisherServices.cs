using my_book.Data.ViewModels;
using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using my_book.Exceptions;

namespace my_book.Data.Services
{
	public class PublisherServices
	{
		private AppDbContext _context;
		public PublisherServices(AppDbContext context)
		{
			_context = context;
		}

		// get all publishers
		public List<Publisher> GetAllPublishers(string sortBy) 
		{
			var allPublishers = _context.Publishers.OrderBy(n => n.FullName).ToList();

			// sorting
			if (!string.IsNullOrEmpty(sortBy)) 
			{
				switch (sortBy) 
				{
					case "name_desc":
						allPublishers = allPublishers.OrderByDescending(n => n.FullName).ToList();
						break;
					default:
						break;
				}
			}
			return allPublishers;
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
			if (StringStartsWithNumber(publisher.FullName)) throw new PublisherNameException("Name starts with number", publisher.FullName) ;

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

		// delete publisher by id
		public void DeletePublisherById(int id)
		{
			var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
			if (_publisher != null)
			{
				_context.Publishers.Remove(_publisher);
				_context.SaveChanges();
			}
			else 
			{
				throw new Exception($"The publishewr with id : {id} does not exists");
			}
		}

		// check if the publisher name starts with a number
		private bool StringStartsWithNumber(string name) 
		{
			if (Regex.IsMatch(name, @"^\d")) return true;
			return false;
		}
	}
}
