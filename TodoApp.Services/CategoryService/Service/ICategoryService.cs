using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;

namespace TodoApp.Services.CategoryService.Service
{
    public interface ICategoryService
    {
        Task<ActionResult> Add(CategoryModel record);

        Task<ActionResult> Deactivate(int id);

        Task<List<CategoryModel>> GetAll();
    }
}