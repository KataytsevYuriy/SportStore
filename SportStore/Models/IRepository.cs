using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Models.Pages;

namespace SportStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product product);
        Product GetProduct(long key);
        void UpdateProduct(Product product);
        void UpdateAll(Product[] products);
        void Delete(Product product);
        PagedList<Product> GetProducts(QueryOptions options);
    }
}
