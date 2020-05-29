using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Repositories;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            var model = _categoryRepository.GetAllCategories(GetCurrentUser());
            return View(model);
        }
        
        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }
    }
}