using dotnet7vue3SpaSimple.Helpers;
using dotnet7vue3SpaSimple.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnet7vue3SpaSimple.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public ValuesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetProductList()
        {
            return Ok(TestDataHelper.Get());
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductInfo value)
        {
            value.Id = Guid.NewGuid();
            TestDataHelper.Add(value);
            return Ok();
        }

        [HttpPost]
        public IActionResult EditProduct([FromBody] ProductInfo value)
        {
            TestDataHelper.Update(value);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductInfo value)
        {
            TestDataHelper.Delete(value.Id);
            return Ok();
        }
    }
}