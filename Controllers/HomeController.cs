using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TinyUrl.Models;

namespace TinyUrl.Controllers
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
            return RedirectToAction("TinyUrlPage", "TinyUrl");
        }
    }
}
