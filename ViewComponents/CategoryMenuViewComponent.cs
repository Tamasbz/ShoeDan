using ShoeStore.Models;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Repository;
namespace ShoeStore.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICatagoryRepo _catagoryRepo;
        public CategoryMenuViewComponent(ICatagoryRepo catagoryRepo)
        {
            _catagoryRepo = catagoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            var category = _catagoryRepo.GetAllCategories().OrderBy(c => c.CategoryId);
            return View(category);
        }
    }
}
