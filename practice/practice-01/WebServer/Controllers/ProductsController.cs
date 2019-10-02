using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WebServer.Data;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public ProductRepository Repository { get; set; }

        //Dependency Injection
        public ProductsController(IHostingEnvironment environment, IConfiguration configuration, ProductRepository productRepository)
        {
            Repository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Repository.Get();
            //return Ok(_data);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByNameResult(string name)
        {
            return Ok(Repository.Get().FirstOrDefault());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = Repository.Get().FirstOrDefault(x => x.Id == id);

            if (result == null)
                return NotFound("No records were found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            Repository.Add(product);

            return Ok();
        }
    }
}