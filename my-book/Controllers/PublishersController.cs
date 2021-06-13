using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Models;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using my_book.Exceptions;
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
			try
			{
				var newPublisher = _publisherService.AddPublisher(publisher);
				return Created(nameof(AddPublisher), newPublisher);
			}
			catch (PublisherNameException ex) 
			{
				return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[HttpGet("get-publisher-by-id/{id}")]
		public ActionResult<Publisher> GetPublisherById(int id) 
		{
			var publisher = _publisherService.GetPublisherById(id);
			if (publisher != null)
			{
				//return Ok(publisher);
				return publisher;
			}
			else 
			{
				//return NotFound();
				return NotFound();
			}
		}

		[HttpGet("get-publisher-books-with-authors/{id}")]
		public IActionResult GetPublisherData(int id) 
		{
			var _response = _publisherService.GetPublisherData(id);
			return Ok(_response);
		}

		[HttpDelete("delete-publisher-by-id/{id}")]
		public IActionResult DeletePublisherById(int id) 
		{
			
			try
			{
				_publisherService.DeletePublisherById(id);
			return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
				
			}
		}
	}
}
