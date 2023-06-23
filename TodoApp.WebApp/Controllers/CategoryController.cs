using Microsoft.AspNetCore.Mvc;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.CategoryService.Service;

namespace TodoApp.WebApp.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryService service;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryAsync()
        {
            try
            {
                var result = await service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync([FromBody] CategoryModel model)
        {
            try
            {
                var result = await service.Add(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> DeactivateCategoryAsync(int id)
        {
            try
            {
                var result = await service.Deactivate(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}