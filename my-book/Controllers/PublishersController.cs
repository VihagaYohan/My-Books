using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_book.ActionResults;
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
		private readonly ILogger<PublishersController> _logger;
		public PublishersController(PublisherServices publisherServices, ILogger<PublishersController>logger)
		{
			_publisherService = publisherServices;
			_logger = logger;
		}

		[HttpGet("get-all-publishers")]
		public IActionResult GetAllPublishers(string sortBy, string searchString) 
		{
			try 
			{
				
				_logger.LogInformation("This is just a log in get all publishers");
				
				var publishers = _publisherService.GetAllPublishers(sortBy,searchString);
				return Ok(publishers);
			} 
			catch (Exception) 
			{
				return BadRequest("Sorry we could not load all publishers");
			}
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
		//public ActionResult<Publisher> GetPublisherById(int id) 
		public CustomActionResults GetPublisherById(int id)
		{
			var publisher = _publisherService.GetPublisherById(id);
			if (publisher != null)
			{
				//return Ok(publisher);

				var _responseObj = new CustomActionResultVM()
				{
					Publisher = publisher
				};
				return new CustomActionResults(_responseObj);
				
				// return publisher;
			}
			else 
			{
				//return NotFound();
				//return NotFound();
				var _responseObj = new CustomActionResultVM()
				{
					Exception = new Exception("This is comming from publisher controller")
				};
				return new CustomActionResults(_responseObj);
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
