using Microsoft.AspNetCore.Mvc;

namespace SpotPicker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        
        private readonly ILogger<ExampleController> _logger;

        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ExampleMethod")]
        public IActionResult ExampleMethod()
        {
            return Ok();
        }

        [HttpGet("ExampleMethod2")]
        public IActionResult ExampleMethod2(string willBeReturned)
        {
            return Ok(willBeReturned);
        }
    }
}