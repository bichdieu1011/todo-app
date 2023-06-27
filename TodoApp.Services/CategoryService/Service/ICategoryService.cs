using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;

namespace TodoApp.Services.CategoryService.Service
{
    public interface ICategoryService
    {
        Task<ActionResult> Add(CategoryModel record, int userId);

        Task<ActionResult> Deactivate(int id, int userId);

        Task<List<CategoryModel>> GetAll(int userId);
    }
}