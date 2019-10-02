using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer.Data
{
    public class ProductRepository
    {
        private readonly List<Product> _data = new List<Product>
        {
            new Product { Name = "Product1", Color = "Blue" },
            new Product { Name = "Product2", Color = "Green" },
            new Product { Name = "Product3", Color = "Brown" },
            new Product { Name = "Product4", Color = "Yellow" },
            new Product { Name = "Product5", Color = "Black" },
            new Product { Name = "Product6", Color = "Magenta" },
            new Product { Name = "Product7", Color = "Cyan" },
            new Product { Name = "Product8", Color = "White" },
            new Product { Name = "Product9", Color = "Orange" },
            new Product { Name = "Product10", Color = "Red" },
        };

        public ProductRepository()
        {
            for (var i = 0; i < _data.Count; i++)
            {
                _data[i].Id = i + 1;
            }
        }

        public List<Product> Get()
        {
            return _data;
        }

        public Product Get(int id)
        {
            return _data.FirstOrDefault(x => x.Id == id);
        }

        public int Add(Product product)
        {
            _data.Add(product);
            product.Id = _data.Count;

            return product.Id;
        }

        public void Update(int id, Product model)
        {
            var match = _data.FirstOrDefault(x => x.Id == id);

            if (match != null)
            {
                _data[_data.IndexOf(match)] = model;
            }
        }

        public void Delete(int id)
        {
            var match = _data.FirstOrDefault(x => x.Id == id);

            if(match != null)
                _data.Remove(match);
        }
    }
}