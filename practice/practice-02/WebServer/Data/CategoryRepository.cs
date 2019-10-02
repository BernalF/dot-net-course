using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Models;

namespace WebServer.Data
{
    public class CategoryRepository
    {
        private readonly AdventureWorksContext _context;

        public CategoryRepository(AdventureWorksContext context)
        {
            _context = context;
        }

        public List<ProductCategory> Get()
        {
            return _context.ProductCategory.ToList();
        }

        public int Add(ProductCategory productCategory)
        {
            _context.Add(productCategory);

           return _context.SaveChanges();
        }
    }
}
