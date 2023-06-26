using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;

namespace TodoApp.Services.CategoryService.Service
{
    public interface ICategoryService
    {
        Task<ActionResult> Add(CategoryModel record, string email);

        Task<ActionResult> Deactivate(int id, string email);

        Task<List<CategoryModel>> GetAll(string email);
    }
}