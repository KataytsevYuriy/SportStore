using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private DataContext conext;
        public CategoryRepository(DataContext ctx)
        {
            conext = ctx;
        }
        public IEnumerable<Category> Categories => conext.Categories;

        public void AddCategory(Category category)
        {
            conext.Categories.Add(category);
            conext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            conext.Categories.Remove(category);
            conext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            conext.Categories.Update(category);
            conext.SaveChanges();
        }
    }
}
