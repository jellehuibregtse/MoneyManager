using Microsoft.AspNetCore.Authorization;
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

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public ViewResult Index()
        {
            return View("TransactionNotFound", null);
        }

        public ViewResult Details(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id);

            if (transaction != null) return View(GetDto(transaction));

            Response.StatusCode = 404;
            return View("TransactionNotFound", id);
        }

        [HttpGet]
        public ViewResult Add(int accountId)
        {
            return View(new TransactionDto {AccountId = accountId});
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
                AccountId = model.AccountId
            };

            _transactionRepository.Add(newTransaction);
            return RedirectToAction("Details", new {id = newTransaction.Id});
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id);

            return View(GetDto(transaction));
        }

        [HttpPost]
        public IActionResult Edit(TransactionDto model)
        {
            if (!ModelState.IsValid) return View();

            var transaction = _transactionRepository.GetTransaction(model.TransactionId);
            transaction.Name = model.Name;
            transaction.Amount = model.Amount;

            _transactionRepository.Update(transaction);

            return RedirectToAction("Details", new {id = transaction.Id});
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var transaction = _transactionRepository.GetTransaction(id);

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

        private static TransactionDto GetDto(Transaction transaction)
        {
            return new TransactionDto
            {
                TransactionId = transaction.Id,
                AccountId = transaction.AccountId,
                Name = transaction.Name,
                Amount = transaction.Amount
            };
        }
    }
}