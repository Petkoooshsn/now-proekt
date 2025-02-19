using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string lastVisitMessage = "Това е първото ви посещение!";
            string cookieName = "LastVisit";

            // Проверка дали има бисквитка с последното посещение
            if (Request.Cookies.TryGetValue(cookieName, out string lastVisit))
            {
                lastVisitMessage = $"Последно посещение: {lastVisit}";
            }

            // Запис на текущата дата и час в бисквитка
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1) // Бисквитката ще е валидна 1 година
            };
            Response.Cookies.Append(cookieName, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), options);

            ViewBag.LastVisit = lastVisitMessage;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}