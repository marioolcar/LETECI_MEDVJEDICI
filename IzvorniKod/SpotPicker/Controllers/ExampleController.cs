using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotPicker.Model;
using SpotPicker.Service.Interface;

namespace SpotPicker.Controllers
{
    public class ExampleController : BaseController
    {
        
        private readonly ILogger<ExampleController> _logger;
        private readonly IExampleTableService _exampleTableService;

        public ExampleController(ILogger<ExampleController> logger, IExampleTableService exampleTableService)
        {
            _logger = logger;
            _exampleTableService = exampleTableService;
        }

        [HttpGet("ExampleMethod")]
        public IActionResult ExampleMethod()
        {
            return Ok();
        }

        [HttpPost("CreateExampleTable")]
        public async Task<IActionResult> CreateExampleTable(ExampleTable exampleTable)
        {
            return Ok(await _exampleTableService.CreateExampleTable(exampleTable));
        }
    }
}