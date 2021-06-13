using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.ViewModels
{
	public class CustomActionResultVM
	{
		public Exception Exception { get; set; }
		// public Object Data { get; set; } 
		public Publisher  Publisher { get; set; }
	}
}
