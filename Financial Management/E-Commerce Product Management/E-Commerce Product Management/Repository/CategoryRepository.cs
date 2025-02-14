using E_Commerce_Product_Management.Data;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;

namespace E_Commerce_Product_Management.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
       
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(p => p.Id).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(p => p.Id == id).FirstOrDefault();
        }  
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(p => p.Id == id);
        }        
        public bool CreateCategory(Category category)
        {
            _context.Add(category);

            return Save();
        }
        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        
    }
}
