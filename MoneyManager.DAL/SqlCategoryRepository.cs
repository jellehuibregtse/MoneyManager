using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.DAL
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public SqlCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category GetCategory(int categoryId, Transaction transaction)
        {
            var category = _context.Categories.Find(categoryId);

            return category.Transaction == transaction ? category : null;
        }

        public IEnumerable<Category> GetAllCategories(Transaction transaction)
        {
            return _context.Categories.Where(category => category.Transaction == transaction);
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        public Category Update(Category updatedCategory)
        {
            var category = _context.Categories.Attach(updatedCategory);
            category.State = EntityState.Modified;
            _context.SaveChanges();

            return updatedCategory;
        }

        public Category Delete(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null) return null;

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}