using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Models;
using ShoeStore.Models.ProductModels;

namespace ShoeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        ShoeStoreContext db = new ShoeStoreContext();
        [HttpGet]
        public IEnumerable<ProductMD> GetAllProduct()
        {
            var product = (from pr in db.Products
                           select new ProductMD
                           {
                               ProductId = pr.ProductId,
                               Name = pr.Name,
                               Image = pr.Image,
                               Price = pr.Price,
                               CategoryId = pr.CategoryId,
                           }).ToList();
            return product;
        }

        [HttpGet("{categoryId}")]
        public IEnumerable<ProductMD> GetProductsByCategory(int categoryID)
        {
            var product = (from pr in db.Products
                           where pr.CategoryId == categoryID
                           select new ProductMD
                           {
                               ProductId = pr.ProductId,
                               Name = pr.Name,
                               Image = pr.Image,
                               Price = pr.Price,
                               CategoryId = pr.CategoryId,
                           }).ToList();
            return product;
        }
    }
}
