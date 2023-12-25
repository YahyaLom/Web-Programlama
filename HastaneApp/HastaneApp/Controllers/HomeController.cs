using HastaneApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HastaneApp.Controllers
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
            return View("Index");
        }

        public IActionResult Love()
        {
            return View();// bu durumda iken love adlı cshtml dosyasını çalıştırır.
            //return View("Test") -> yazdığımızda Test.cshtml adlı Home dosyası altında ki view ı açar.
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}