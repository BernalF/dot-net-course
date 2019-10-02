using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDBContext _dbContext;

        public AuthorsController(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Authors
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Authors>>> GetAuthors()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Authors>> GetAuthors(int id)
        {
            var authors = await _dbContext.Authors.FindAsync(id);

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAuthors(int id, Authors authors)
        {
            if (id != authors.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(authors).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Authors>> PostAuthors(Authors authors)
        {
            _dbContext.Authors.Add(authors);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetAuthors", new { id = authors.Id }, authors);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Authors>> DeleteAuthors(int id)
        {
            var authors = await _dbContext.Authors.FindAsync(id);
            if (authors == null)
            {
                return NotFound();
            }

            _dbContext.Authors.Remove(authors);
            await _dbContext.SaveChangesAsync();

            return authors;
        }

        private bool AuthorsExists(int id)
        {
            return _dbContext.Authors.Any(e => e.Id == id);
        }
    }
}
