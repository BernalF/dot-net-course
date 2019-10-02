using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Entities.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly BookStoreDBContext _dbContext;

        public ReviewsController(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Reviews
        [HttpGet]
        [Authorize]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return await _dbContext.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Reviews>> GetReviews(int id)
        {
            var reviews = await _dbContext.Reviews.FindAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReviews(int id, Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(reviews).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
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

        // POST: api/Reviews
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Reviews>> PostReviews(Reviews reviews)
        {
            _dbContext.Reviews.Add(reviews);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Reviews>> DeleteReviews(int id)
        {
            var reviews = await _dbContext.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }

            _dbContext.Reviews.Remove(reviews);
            await _dbContext.SaveChangesAsync();

            return reviews;
        }

        private bool ReviewsExists(int id)
        {
            return _dbContext.Reviews.Any(e => e.Id == id);
        }
    }
}
