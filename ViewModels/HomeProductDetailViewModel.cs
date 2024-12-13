using ShoeStore.Models;
namespace ShoeStore.ViewModels
{
    public class HomeProductDetailViewModel
    {
        public Product product {  get; set; }
        public List<ProductImageDetail> productImageDetail { get; set; }
    }
}
