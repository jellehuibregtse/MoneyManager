using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Web.ViewModels;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            var model = _accountRepository.GetAllAccounts(GetCurrentUser());
            return View(model);
        }

        public ViewResult Details(int id)
        {
            var account = _accountRepository.GetAccount(id, GetCurrentUser());

            if (account != null) return View(GetDto(account));
            
            Response.StatusCode = 404;
            return View("AccountNotFound", id);
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var account = _accountRepository.GetAccount(id, GetCurrentUser());

            if (account != null) return View(GetDto(account));

            Response.StatusCode = 404;
            return View("AccountNotFound", id);
        }

        [HttpPost]
        public IActionResult Delete(AccountDto model)
        {
            if (!ModelState.IsValid) return View();

            _accountRepository.Delete(model.AccountId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AccountDto model)
        {
            if (!ModelState.IsValid) return View();

            var newAccount = new Account
            {
                Id = model.AccountId,
                Name = model.Name,
                InitialBalance = model.Balance,
                ApplicationUser = GetCurrentUser()
            };

            _accountRepository.Add(newAccount);
            return RedirectToAction("Details", new {id = newAccount.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            return View(GetDto(_accountRepository.GetAccount(id, GetCurrentUser())));
        }

        [HttpPost]
        public IActionResult Edit(AccountDto model)
        {
            if (!ModelState.IsValid) return View();

            var account = _accountRepository.GetAccount(model.AccountId, GetCurrentUser());
            account.Name = model.Name;
            account.InitialBalance = model.Balance;

            _accountRepository.Update(account);
            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

        private static AccountDto GetDto(Account account)
        {
            return new AccountDto
            {
                AccountId = account.Id,
                Balance = account.InitialBalance,
                Name = account.Name,
                Transactions = account.Transactions ?? new List<Transaction>()
            };
        }
    }
}