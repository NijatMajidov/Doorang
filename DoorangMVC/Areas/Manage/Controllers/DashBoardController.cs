using Microsoft.AspNetCore.Mvc;

namespace DoorangMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
