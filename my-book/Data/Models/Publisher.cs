using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Models
{
	public class Publisher
	{
		public int Id { get; set; }
		public String FullName { get; set; }

		// navigation properties
		public List<Book> Books { get; set; }
	}
}
