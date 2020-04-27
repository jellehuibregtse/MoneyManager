using Microsoft.AspNetCore.Mvc;
using MoneyManager.Web.Models;

namespace MoneyManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}