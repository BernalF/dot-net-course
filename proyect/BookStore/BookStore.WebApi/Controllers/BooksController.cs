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
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository _booksRepository;

        public BooksController(BooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        // GET: api/Books
        [HttpGet]
        [Authorize]
        [EnableQuery]
        public async Task<ActionResult<IList<Books>>> Get()
        {
            var books = await _booksRepository.Get();

            if (books == null)
                return NotFound();

            Response.Headers.Add("X-Total-Count", new StringValues(books.Count.ToString()));
            Response.Headers.Add("page-size", new StringValues(books.Count.ToString()));

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Books>> GetById([FromRoute] int? id)
        {
            var books = await _booksRepository.GetById(id);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // POST: api/Books
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Books>> Create([FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _booksRepository.Create(books);

            return CreatedAtAction("Create", new { id = result }, books);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Books>> Update([FromRoute] int id, [FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = await _booksRepository.GetById(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Author = books.Author;
            model.Category = books.Category;
            model.InsertDate = books.InsertDate;
            model.Reviews = books.Reviews;
            model.AuthorId = books.AuthorId;
            model.CategoryId = books.CategoryId;
            model.Name = books.Name;

            var result = await _booksRepository.Update(id, model);

            return CreatedAtAction("Update", new {id = result}, model);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var books = await _booksRepository.GetById(id);

            if (books == null)
            {
                return NotFound();
            }

            await _booksRepository.Delete(id, books);

            return Ok();
        }
    }
}
