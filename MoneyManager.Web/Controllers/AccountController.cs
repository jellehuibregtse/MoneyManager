using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Domain.Models;
using MoneyManager.Domain.Repositories;
using MoneyManager.Web.ViewModels;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject the interface using constructor injection
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

        public ViewResult Details(int? id)
        {
            var account = _accountRepository.GetAccount(id.Value, GetCurrentUser());

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

            accountDetailsViewModel.Account.Transactions ??= new List<Transaction>();
            
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
                Balance = model.Balance,
                ApplicationUser = GetCurrentUser()
            };

            _accountRepository.Add(newAccount);
            return RedirectToAction("Details", new {id = newAccount.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var account = _accountRepository.GetAccount(id, GetCurrentUser());
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

            var account = _accountRepository.GetAccount(model.Id, GetCurrentUser());
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

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }
    }
}