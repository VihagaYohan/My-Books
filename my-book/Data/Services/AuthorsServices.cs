using my_book.Data.ViewModels;
using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
	public class AuthorsServices
	{
		private AppDbContext _context;
		public AuthorsServices(AppDbContext context)
		{
			_context = context;
		}

		// add author
		public void AddAuthor(AuthorVM author) 
		{
			var _author = new Author()
			{
				FullName = author.FullName
			};

			_context.Authors.Add(_author);
			_context.SaveChanges();
		}

	}
}
