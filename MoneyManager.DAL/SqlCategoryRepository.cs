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

        public Category GetCategory(int categoryId, ApplicationUser applicationUser)
        {
            return _context.Categories
                .Include(category => category.Transactions)
                .Include(category => category.ApplicationUser)
                .SingleOrDefault(category =>
                category.Id == categoryId && category.ApplicationUser == applicationUser
            );
        }

        public IEnumerable<Category> GetAllCategories(ApplicationUser applicationUser)
        {
            return _context.Categories
                .Include(category => category.Transactions)
                .Include(category => category.ApplicationUser)
                .Where(category => category.ApplicationUser == applicationUser);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category updatedCategory)
        {
            var category = _context.Categories.Attach(updatedCategory);
            category.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null) return;

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}