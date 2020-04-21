using Microsoft.AspNetCore.Mvc;
using MoneyManagerWeb.Models;

namespace MoneyManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}