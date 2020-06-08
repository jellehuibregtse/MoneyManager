using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Web.ViewModels;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            return View("TransactionNotFound", null);
        }

        public ViewResult Details(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id, GetCurrentUser());

            if (transaction != null) return View(GetDto(transaction));

            Response.StatusCode = 404;
            return View("TransactionNotFound", id);
        }

        [HttpGet]
        public ViewResult Add(int accountId)
        {
            return View(new TransactionDto
            {
                AccountId = accountId,
                Categories =
                    new SelectList(
                        _categoryRepository.GetAllCategories(_userManager.GetUserAsync(User).Result).ToList(), "Id",
                        "Name")
            });
        }

        [HttpPost]
        public IActionResult Add(TransactionDto model)
        {
            if (!ModelState.IsValid) return View();

            var newTransaction = new Transaction
            {
                Id = model.TransactionId,
                Name = model.Name,
                Amount = model.Amount,
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                TransactionDate = DateTime.Now,
                ApplicationUser = GetCurrentUser()
            };

            _transactionRepository.Add(newTransaction);
            return RedirectToAction("Details", new {id = newTransaction.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id, GetCurrentUser());

            return View(GetDto(transaction));
        }

        [HttpPost]
        public IActionResult Edit(TransactionDto model)
        {
            if (!ModelState.IsValid) return View();

            var transaction = _transactionRepository.GetTransaction(model.TransactionId, GetCurrentUser());
            transaction.Name = model.Name;
            transaction.Amount = model.Amount;
            transaction.CategoryId = model.CategoryId == 0 ? null : model.CategoryId;

            _transactionRepository.Update(transaction);

            return RedirectToAction("Details", new {id = transaction.Id});
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id, GetCurrentUser());

            if (transaction != null) return View(GetDto(transaction));

            Response.StatusCode = 404;
            return View("TransactionNotFound", id);
        }

        [HttpPost]
        public IActionResult Delete(TransactionDto model)
        {
            if (!ModelState.IsValid) return View();

            _transactionRepository.Delete(model.TransactionId);

            return RedirectToAction("Details", "Account", new {id = model.AccountId});
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

        private TransactionDto GetDto(Transaction transaction)
        {
            return new TransactionDto
            {
                TransactionId = transaction.Id,
                AccountId = transaction.AccountId,
                Name = transaction.Name,
                Amount = transaction.Amount,
                Categories =
                    new SelectList(
                        _categoryRepository.GetAllCategories(_userManager.GetUserAsync(User).Result).ToList(), "Id",
                        "Name")
            };
        }
    }
}