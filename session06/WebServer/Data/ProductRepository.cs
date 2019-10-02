using AdventureWorks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.ViewModels;

namespace WebServer.Data
{
    public class ProductRepository
    {
        public AdventureWorksContext DbContext { get; }

        public ProductRepository(AdventureWorksContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<Product> Get()
        {
            return DbContext.Product.AsQueryable();
        }

        public Product Get(int id)
        {
            return DbContext.Product.Include(p => p.ProductCategory)
                            .FirstOrDefault();
        }

        public int Add(ProductViewModel viewModel)
        {
            var model = new Product();
            model.Color = viewModel.Color;
            model.Name = viewModel.Name;

            model.ModifiedDate = DateTime.Now;
            model.SellStartDate = DateTime.Now;
            model.ProductNumber = viewModel.ProductNumber;

            DbContext.Product.Add(model);

            DbContext.SaveChanges();

            return model.ProductId;
        }

        public void Update(int id, Product model)
        {
           var match = DbContext.Product.FirstOrDefault(m => m.ProductId == id);

            if (match != null)
            {
                model.ProductId = id;
                DbContext.Product.Update(match);

                DbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var match = DbContext.Product.FirstOrDefault(m => m.ProductId == id);

            if (match != null)
            {
                DbContext.Product.Remove(match);
            }
        }
    }
}
