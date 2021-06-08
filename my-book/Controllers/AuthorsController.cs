﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private AuthorsServices _authorsService;
		public AuthorsController(AuthorsServices authorsServices)
		{
			_authorsService = authorsServices;
		}


		[HttpPost("add-author")]
		public IActionResult AddAuthor([FromBody] AuthorVM author) 
		{
			_authorsService.AddAuthor(author);
			return Ok();
		}
	}
}
