using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Models;
using DoAnPhanMem.Data;
namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly dbSportStoreContext _context;

        public OrderController(dbSportStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.DonHang.ToList();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.DonHang.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
