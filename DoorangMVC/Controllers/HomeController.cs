using DoorangMVC.Business.Services.Abstract;
using DoorangMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoorangMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExploreService _exploreService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IExploreService exploreService)
        {
            _logger = logger;
            _exploreService = exploreService;
        }

        public IActionResult Index()
        {
            return View(_exploreService.GetAllExplores());
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