using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Web.ViewModels;

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

        public ViewResult Details(int id)
        {
            var category = _categoryRepository.GetCategory(id, GetCurrentUser());

            if (category != null) return View(GetDto(category));

            Response.StatusCode = 404;
            return View("CategoryNotFound", id);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var category = _categoryRepository.GetCategory(id, GetCurrentUser());

            if (category != null) return View(GetDto(category));

            Response.StatusCode = 404;
            return View("CategoryNotFound", id);
        }

        [HttpPost]
        public IActionResult Delete(CategoryDto model)
        {
            if (!ModelState.IsValid) return View();

            _categoryRepository.Delete(model.CategoryId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDto model)
        {
            if (!ModelState.IsValid) return View();

            var newCategory = new Category
            {
                Id = model.CategoryId,
                Name = model.Name,
                ApplicationUser = GetCurrentUser()
            };

            _categoryRepository.Add(newCategory);
            return RedirectToAction("Details", new {id = newCategory.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            return View(GetDto(_categoryRepository.GetCategory(id, GetCurrentUser())));
        }

        [HttpPost]
        public IActionResult Edit(CategoryDto model)
        {
            if (!ModelState.IsValid) return View();

            var category = _categoryRepository.GetCategory(model.CategoryId, GetCurrentUser());
            category.Name = model.Name;

            _categoryRepository.Update(category);
            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

        private static CategoryDto GetDto(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.Id,
                Name = category.Name
            };
        }
    }
}