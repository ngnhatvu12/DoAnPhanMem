using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Models;
using DoAnPhanMem.Data;
namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbSportStoreContext _context;

        public ProductController(dbSportStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.SanPham.ToList(); // Lấy danh sách sản phẩm từ database
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SanPham product)
        {
            if (ModelState.IsValid)
            {
                _context.SanPham.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.SanPham.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(SanPham product)
        {
            if (ModelState.IsValid)
            {
                _context.SanPham.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
