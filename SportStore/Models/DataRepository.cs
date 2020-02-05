using Microsoft.EntityFrameworkCore;
using SportStore.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class DataRepository : IRepository
    {
        private DataContext context;
        public DataRepository(DataContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products => context.Products.Include(c=>c.Category).ToArray();


        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

       
        public Product GetProduct(long key)
        {
            return context.Products.Include(c=>c.Category).First(k=> k.Id==key);
        }

        void IRepository.UpdateProduct(Product product)
        {
            //conext.Products.Update(product);
            Product p = context.Products.Find(product.Id);
            p.Name = product.Name;
            //p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            p.CategoryId = product.CategoryId;
            context.SaveChanges();
        }

        public void UpdateAll (Product[] products)
        {
            //context.Products.UpdateRange(products);
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = context.Products.Where(p => data.Keys.Contains(p.Id));
            foreach(Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public PagedList<Product> GetProducts(QueryOptions options)
        {
            return new PagedList<Product>(context.Products.Include(p => p.Category), options);
        }
    }
}
