using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}