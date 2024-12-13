using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using ShoeStore.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace ShoeStore.Controllers
{
    public class ShopController : Controller
    {
        ShoeStoreContext db = new ShoeStoreContext();
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        //[Authentication]
        public IActionResult Index(int? page)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            int pageSize = 16;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult SanPhamTheoLoai(int categoryId, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.Products.AsNoTracking().Where(x => x.CategoryId == categoryId).OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstSanPham, pageNumber, pageSize);
            ViewBag.CategoryId = categoryId;
            return View(lst);
        }

        public IActionResult ChiTietSanPham(int productId)
        {
            var product = db.Products.SingleOrDefault(x => x.ProductId == productId);
            var productImage = db.ProductImageDetails.Where(x => x.ProductId == productId).ToList();
            ViewBag.ProductImage = productImage;
            return View(product);
        }

        public IActionResult ProductDetail(int productId)
        {
            var product = db.Products.SingleOrDefault(x => x.ProductId == productId);
            var productImage = db.ProductImageDetails.Where(x => x.ProductId == productId).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel { product = product, productImageDetail = productImage };
            return View(homeProductDetailViewModel);
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
