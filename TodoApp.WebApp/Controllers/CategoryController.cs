using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.CategoryService.Service;
using TodoApp.WebApp.Identity;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

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
        private readonly IIdentityService identityService;

        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService service,
            IIdentityService identityService)
        {
            this.logger = logger;
            this.service = service;
            this.identityService = identityService;
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryAsync()
        {
            var result = await service.GetAll(identityService.UserId);
            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync([FromBody] CategoryModel model)
        {
            var result = await service.Add(model, identityService.UserId);
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult> DeactivateCategoryAsync(int id)
        {
            var result = await service.Deactivate(id, identityService.UserId);
            return Ok(result);
        }
    }
}