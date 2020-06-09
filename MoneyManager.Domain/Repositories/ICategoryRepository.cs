using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int categoryId, ApplicationUser applicationUser);
        IEnumerable<Category> GetAllCategories(ApplicationUser applicationUser);
        void Add(Category category);
        void Update(Category updatedCategory);
        void Delete(int categoryId);
    }
}