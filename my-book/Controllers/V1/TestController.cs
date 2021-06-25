﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers.V1
{
	[ApiVersion("1.0")]
	//[Route("api/[controller]")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get() 
		{
			return Ok("This is test controller V1");
		}
	}
}
