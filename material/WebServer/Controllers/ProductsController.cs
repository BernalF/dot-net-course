using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using WebServer.Data;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public ProductRepository _productRepository { get; set; }

        //Dependency Injection
        public ProductsController(IHostingEnvironment environment, IConfiguration configuration, ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ObjectResult Get()
        {
            var data = _productRepository.Get();

            var name = Request.Query.Keys.FirstOrDefault(key => key == "name");

            if (!string.IsNullOrEmpty(name) )
            {
                name = Request.Query[name].ToString();

                data = data.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var result = new ObjectResult(data)
            {
                StatusCode = (int) HttpStatusCode.OK
            };

            Response.Headers.Add("X-Total-Count", new StringValues(data.Count.ToString()));
            Response.Headers.Add("page-size", new StringValues(5.ToString()));

            return result;
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByNameResult(string name)
        {
            return Ok(_productRepository.Get().FirstOrDefault());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productRepository.Get(id);

            if (result == null)
                return NotFound("No records were found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _productRepository.Add(product);

            return CreatedAtAction(nameof(GetById), new { Id = id }, product);
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _productRepository.Get(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Name = product.Name;
            model.Color = product.Color;

            _productRepository.Update(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_productRepository.Get(id) == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            return Ok();
        }
    }
}