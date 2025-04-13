using Microsoft.AspNetCore.Mvc;

namespace BankCreditSystem.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Метод для отображения страницы Privacy
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
