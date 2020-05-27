using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Web.ViewModels;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public TransactionController(ITransactionRepository transactionRepository, UserManager<ApplicationUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _userManager = userManager;
        }
        
        public ViewResult Details(int? id)
        {
            var transaction = _transactionRepository.GetTransaction(id.Value);

            if (transaction == null)
            {
                Response.StatusCode = 404;
                return View("TransactionNotFound", id.Value);
            }
            
            var transactionDetailsDto = new TransactionDetailsDto()
            {
                Transaction = transaction,
                PageTitle = "Transaction Details"
            };
            
            return View(transactionDetailsDto);
        }
        
        [HttpGet]
        public ViewResult Add(int accountId)
        {
            return View(new TransactionAddDto {AccountId = accountId});
        }

        [HttpPost]
        public IActionResult Add(TransactionAddDto model)
        {
            if (!ModelState.IsValid) return View();

            var newTransaction = new Transaction()
            {
                Id = model.TransactionId,
                Name = model.Name,
                Amount = model.Amount,
                AccountId = model.AccountId,
                //Account = _userManager.GetUserAsync(User).Result.Accounts.First(account => account.Id == model.AccountId)
            };
            
            _transactionRepository.Add(newTransaction);
            return RedirectToAction("Details", new {id = newTransaction.Id});
        }
    }
}