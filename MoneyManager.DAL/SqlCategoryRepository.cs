﻿using System.Collections.Generic;
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
            return _context.Categories.Include(category => category.Transaction)
                .Include(category => category.ApplicationUser)
                .SingleOrDefault(category =>
                category.Id == categoryId && category.ApplicationUser == applicationUser
            );
        }

        public IEnumerable<Category> GetAllCategories(ApplicationUser applicationUser)
        {
            return _context.Categories.Include(category => category.Transaction)
                .Include(category => category.ApplicationUser)
                .Where(category => category.ApplicationUser == applicationUser);
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
            throw new System.NotImplementedException();
        }
    }
}