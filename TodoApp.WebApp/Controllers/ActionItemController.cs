using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TodoApp.Services.ActionItemService;
using TodoApp.Services.ActionItemService.Models;
using TodoApp.WebApp.Identity;
using static TodoApp.Services.Constant;

namespace TodoApp.WebApp.Controllers
{
    [Route("actionItem")]
    [ApiController]
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class ActionItemController : Controller
    {
        private readonly ILogger<ActionItemController> logger;
        private readonly IActionItemServices service;
        private readonly IIdentityService identityService;

        public ActionItemController(ILogger<ActionItemController> logger,
            IActionItemServices service, IIdentityService identityService)
        {
            this.logger = logger;
            this.service = service;
            this.identityService = identityService;
        }

        [Route("widget")]
        [HttpGet]
        public async Task<ActionResult> GetAllActionItemByWidgetAsync(int categoryId,
            TaskWidgetType type, int skip, int take, string? sortBy, string? sortdirection)
        {
            var result = await service.GetAllByWidget(categoryId, type, skip, take, sortBy, sortdirection, identityService.UserId);
            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddActionItemAsync([FromBody] ActionItemModel model)
        {
            var result = await service.Add(model, identityService.UserId);
            return Ok(result);
        }

        [Route("editstatus/{id:long}")]
        [HttpPut]
        public async Task<ActionResult> UpdateActionItemStatusAsync(long id, [FromBody] UpdateActionItemStatus model)
        {
            model.Id = id;
            var result = await service.Edit(model, identityService.UserId);
            return Ok(result);
        }

        [Route("{id:long}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteActionItemAsync(long id)
        {
            var result = await service.Delete(id, identityService.UserId);
            return Ok(result);
        }
    }
}