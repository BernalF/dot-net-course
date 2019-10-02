using AdventureWorks.Models;
using System;
using System.Linq;

namespace WebServer.Data
{
    public class CategoryRepository
    {
        public AdventureWorksContext Context { get; }

        public CategoryRepository(AdventureWorksContext context)
        {
            Context = context;
        }

        public ProductCategory[] Get()
        {
            return Context.ProductCategory.Take(10).ToArray();
        }

        public ProductCategory Get(int id)
        {
            return Context.ProductCategory.Find(id);
        }
    }
}
