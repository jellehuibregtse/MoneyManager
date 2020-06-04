using System;
using System.Collections.Generic;
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

        public ViewResult Details(int id, string sort = "all")
        {
            var account = _accountRepository.GetAccount(id, GetCurrentUser());

            if (account == null)
            {
                Response.StatusCode = 404;
                return View("AccountNotFound", id);
            }

            account.Transactions = sort switch
            {
                "month" => account.Transactions
                    .Where(transaction => transaction.TransactionDate.Month.Equals(DateTime.Now.Month))
                    .ToList(),
                "week" => account.Transactions.Where(transaction =>
                        Math.Abs(DateTime.Now.Day - transaction.TransactionDate.Day) < 7 &&
                        transaction.TransactionDate.Month.Equals(DateTime.Now.Month))
                    .ToList(),
                _ => account.Transactions
            };

            ViewData["sort"] = sort;

            account.Transactions = account.Transactions.OrderByDescending(transaction => transaction.TransactionDate).ToList();

            return View(GetDto(account));
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
                InitialBalance = model.InitialBalance,
                ApplicationUser = GetCurrentUser()
            };

            _accountRepository.Add(newAccount);
            return RedirectToAction("Details", new {id = newAccount.Id, sort = "all"});
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
            account.InitialBalance = model.InitialBalance;

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
                InitialBalance = account.InitialBalance,
                Name = account.Name,
                Transactions = account.Transactions ?? new List<Transaction>()
            };
        }
    }
}