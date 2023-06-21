using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;

namespace TodoApp.Services.CategoryService.Service
{
    public interface ICategoryService
    {
        Task<ActionResult> Add(CategoryModel record);
        Task<ActionResult> Edit(CategoryModel record);
        Task<ActionResult> Deactivate(int id);
        Task<ActionResult> Activate(int id);
        Task<List<CategoryModel>> GetAll();
        
    }
}
