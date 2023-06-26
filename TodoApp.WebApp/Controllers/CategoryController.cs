using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.CategoryService.Service;
using TodoApp.WebApp.Identity;

namespace TodoApp.WebApp.Controllers
{
    [Route("category")]
    [ApiController]
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryService service;
        readonly string _email;
        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService service,
            IIdentityService identityService)
        {
            this.logger = logger;
            this.service = service;
            _email = identityService.GetUserIdentityEmail().Result;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryAsync()
        {
            var result = await service.GetAll(_email);
            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync([FromBody] CategoryModel model)
        {
            var result = await service.Add(model, _email);
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> DeactivateCategoryAsync(int id)
        {
            var result = await service.Deactivate(id, _email);
            return Ok(result);
        }
    }
}