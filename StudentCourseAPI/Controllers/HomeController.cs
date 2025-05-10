using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public HomeController( IMessageService messageService) 
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var message = _messageService.GetWelcomeMessage();
                Console.WriteLine("Returned message: " + message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception: " + ex.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

    }
}
