using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Models;
using Microsoft.AspNetCore.Mvc;
using WebServer.Data;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _categoryRepository.Get();

            if (result == null)
                return NotFound("No records were found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCategory productCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(_categoryRepository.Add(productCategory));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}