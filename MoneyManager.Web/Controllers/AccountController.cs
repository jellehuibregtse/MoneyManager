using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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

        // Inject the interface using constructor injection
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ViewResult Index()
        {
            var model = _accountRepository.GetAllAccounts();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            var account = _accountRepository.GetAccount(id.Value);

            if (account == null)
            {
                Response.StatusCode = 404;
                return View("AccountNotFound", id.Value);
            }

            var accountDetailsViewModel = new AccountDetailsDto()
            {
                Account = account,
                PageTitle = "Account Details"
            };

            return View(accountDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AccountCreateDto model)
        {
            if (!ModelState.IsValid) return View();

            var newAccount = new Account
            {
                Id = model.Id,
                Name = model.Name,
                Balance = model.Balance
            };

            _accountRepository.Add(newAccount);
            return RedirectToAction("Details", new {id = newAccount.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var account = _accountRepository.GetAccount(id);
            var accountEditDto = new AccountEditDto
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance
            };

            return View(accountEditDto);
        }

        [HttpPost]
        public IActionResult Edit(AccountEditDto model)
        {
            if (!ModelState.IsValid) return View();

            var account = _accountRepository.GetAccount(model.Id);
            account.Name = model.Name;
            account.Balance = model.Balance;

            var newAccount = new Account
            {
                Id = model.Id,
                Name = model.Name,
                Balance = model.Balance
            };

            _accountRepository.Update(account);
            return RedirectToAction("Index");
        }
    }
}