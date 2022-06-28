using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]s")]
    [ApiController]
    public class PlatformController : ControllerBase
    {

        [HttpPost]
        public ActionResult TestInboudConnection()
        {
            string feedback = ">>>Inbound Post Command Service";
            Console.WriteLine($"{feedback}");

            return Ok(feedback);
        }
    }
}
