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
    public class HomeController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IAccountRepository accountRepository, ITransactionRepository transactionRepository, UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            var transactions = _transactionRepository.GetAllTransactions(GetCurrentUser()).ToList();
            transactions = transactions.OrderByDescending(transaction => transaction.TransactionDate).ToList();
            transactions.GetRange(0, transactions.Count >= 10 ? 10 : transactions.Count);

            var accounts = _accountRepository.GetAllAccounts(GetCurrentUser());

            var model = new HomeDto
            {
                Transactions = transactions,
                Accounts = accounts
            };
            
            return View(model);
        }
        
        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }
    }
}