using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Entities.Models;
using BookStore.WebApi.Repositories;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // GET: api/Categories
        [HttpGet]
        [Authorize]
        [EnableQuery]
        public async Task<ActionResult<IList<Categories>>> Get()
        {
            var categories = await _categoriesRepository.Get();

            if (categories == null)
                return NotFound();

            Response.Headers.Add("X-Total-Count", new StringValues(categories.Count.ToString()));
            Response.Headers.Add("page-size", new StringValues(categories.Count.ToString()));

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Categories>> GetById([FromRoute] int? id)
        {
            var categories = await _categoriesRepository.GetById(id);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        // POST: api/Categories
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Categories>> Create([FromBody] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categoriesRepository.Create(categories);

            return CreatedAtAction("Create", new { id = result }, categories);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Categories>> Update([FromRoute] int id, [FromBody] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = await _categoriesRepository.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Name = categories.Name;
            model.InsertDate = categories.InsertDate;

            var result = await _categoriesRepository.Update(id, model);

            return CreatedAtAction("Update", new {id = result}, model);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var categories = await _categoriesRepository.GetById(id);

            if (categories == null)
            {
                return NotFound();
            }

            await _categoriesRepository.Delete(id, categories);

            return Ok();
        }
    }
}
