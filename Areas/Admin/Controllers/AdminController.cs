using Microsoft.AspNetCore.Mvc;
using DoAnPhanMem.Models;
using System.Linq;
using DoAnPhanMem.Data;
namespace DoAnPhanMem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly dbSportStoreContext _context;

        public AdminController(dbSportStoreContext context)
        {
            _context = context;
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        // Hiển thị danh sách sản phẩm
        public IActionResult Index()
        {
            var products = _context.SanPham.ToList(); // Lấy danh sách sản phẩm từ database
            return View(products);
        }

        // Tạo sản phẩm mới - GET
        public IActionResult Create()
        {
            return View();
        }

        // Tạo sản phẩm mới - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.SanPham.Add(sanPham);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // Sửa sản phẩm - GET
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = _context.SanPham.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // Sửa sản phẩm - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.SanPham.Update(sanPham);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // Xóa sản phẩm - GET
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = _context.SanPham.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // Xóa sản phẩm - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var sanPham = _context.SanPham.Find(id);
            _context.SanPham.Remove(sanPham);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
