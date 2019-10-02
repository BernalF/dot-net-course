
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Entities;
using BookStore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Repositories
{
    public class BooksRepository
    {
        private readonly BookStoreDBContext _dbContext;

        public BooksRepository(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Books>> Get()
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<Books> GetById(int? id)
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> Create(Books books)
        {
            _dbContext.Books.Add(books);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(int? id, Books books)
        {
            _dbContext.Entry(books).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int? id, Books books)
        {
            _dbContext.Books.Remove(books);

            await _dbContext.SaveChangesAsync();
        }
    }
}