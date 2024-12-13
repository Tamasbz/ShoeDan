using ShoeStore.Models;
namespace ShoeStore.Repository
{
    public interface ICatagoryRepo
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(String categoryId);
        Category GetCategory(String categoryId);
        IEnumerable<Category> GetAllCategories();
    }
}
