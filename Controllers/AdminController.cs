using Microsoft.AspNetCore.Mvc;

namespace DoAnPhanMem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
