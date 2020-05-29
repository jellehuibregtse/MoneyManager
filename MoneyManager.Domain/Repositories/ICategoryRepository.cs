using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int categoryId, ApplicationUser applicationUser);
        IEnumerable<Category> GetAllCategories(ApplicationUser applicationUser);
        Category Add(Category category);
        Category Update(Category updatedCategory);
        Category Delete(int categoryId);
    }
}