using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Entities;
using BookStore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApi.Repositories
{
    public class CategoriesRepository
    {
        private readonly BookStoreDBContext _dbContext;

        public CategoriesRepository(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Categories>> Get()
        {
            return await _dbContext.Categories.AsQueryable().ToListAsync();
        }

        public async Task<Categories> GetById(int? id)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> Create(Categories categories)
        {
            _dbContext.Categories.Add(categories);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(int? id, Categories categories)
        {
            _dbContext.Entry(categories).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int? id, Categories categories)
        {
            _dbContext.Categories.Remove(categories);

            await _dbContext.SaveChangesAsync();
        }
    }
}