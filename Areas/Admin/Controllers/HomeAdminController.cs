using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using X.PagedList;

namespace ShoeStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        ShoeStoreContext db = new ShoeStoreContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("listofproducts")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("addproduct")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "Name");
            return View();
        }

        [Route("addproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(product);
        }

        [Route("updateproduct")]
        [HttpGet]
        public IActionResult UpdateProduct(int productId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "Name");
            var product = db.Products.Find(productId);
            return View(product);
        }

        [Route("updateproduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(product);
        }

        [Route("deleteproduct")]
        [HttpGet]
        public IActionResult DeleteProduct(int productId)
        {
            TempData["Message"] = "";

            try
            {
                // Tìm sản phẩm cần xóa
                var product = db.Products.Find(productId);

                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();

                    TempData["Message"] = "Sản phẩm và dữ liệu liên quan đã được xóa thành công.";
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm.";
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông tin lỗi chi tiết
                TempData["Message"] = "Đã xảy ra lỗi khi xóa sản phẩm: " + ex.InnerException?.Message ?? ex.Message;
            }

            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }


    }
}
