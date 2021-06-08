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

		// add publishers
		public void AddPublisher(PublisherVM publisher) 
		{
			var _publisher = new Publisher()
			{
				FullName = publisher.FullName
			};

			_context.Publishers.Add(_publisher);
			_context.SaveChanges();
		}
	}
}
