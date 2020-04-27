using Microsoft.AspNetCore.Mvc;
using MoneyManager;
using MoneyManager.Web.Models;
using MoneyManager.Web.ViewModels;

namespace MoneyManager.Web.Controllers
{
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
            var accountDetailsViewModel = new AccountDetailsViewModel()
            {
                Account = _accountRepository.GetAccount(id ?? 1),
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
        public IActionResult Create(Account account)
        {
            if (!ModelState.IsValid) return View();
            
            var newAccount = _accountRepository.Add(account);
            return RedirectToAction("Index");

        }
    }
    
}