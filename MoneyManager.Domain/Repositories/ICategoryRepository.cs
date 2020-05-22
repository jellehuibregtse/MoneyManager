using System.Collections.Generic;
using MoneyManager.Models;

namespace MoneyManager.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int categoryId, Transaction transaction);
        IEnumerable<Category> GetAllCategories(Transaction transaction);
        Category Add(Category category);
        Category Update(Category updatedCategory);
        Category Delete(int categoryId);
    }
}