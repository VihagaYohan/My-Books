using Microsoft.AspNetCore.Http;
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
	public class PublishersController : ControllerBase
	{
		private PublisherServices _publisherService;
		public PublishersController(PublisherServices publisherServices)
		{
			_publisherService = publisherServices;
		}

		[HttpPost("add-publishers")]
		public IActionResult AddPublisher([FromBody]PublisherVM publisher) 
		{
			_publisherService.AddPublisher(publisher);
			return Ok();
		}

		[HttpGet("get-publisher-books-with-authors/{id}")]
		public IActionResult GetPublisherData(int id) 
		{
			var _response = _publisherService.GetPublisherData(id);
			return Ok(_response);
		}
	}
}
