using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TodoApp.Services.ActionItemService;
using TodoApp.Services.ActionItemService.Models;
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

        public ActionItemController(ILogger<ActionItemController> logger, IActionItemServices service)
        {
            this.logger = logger;
            this.service = service;
        }

        [Route("widget")]
        [HttpGet]
        public async Task<ActionResult> GetAllActionItemByWidgetAsync(int categoryId,
            TaskWidgetType type, int skip, int take, string? sortBy, string? sortdirection)
        {
            try
            {
                var result = await service.GetAllByWidget(categoryId, type, skip, take, sortBy, sortdirection);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddActionItemAsync([FromBody] ActionItemModel model)
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

        [Route("editstatus/{id:long}")]
        [HttpPut]
        public async Task<ActionResult> UpdateActionItemStatusAsync(long id, [FromBody] UpdateActionItemStatus model)
        {
            try
            {
                model.Id = id;
                var result = await service.Edit(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [Route("{id:long}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteActionItemAsync(long id)
        {
            try
            {
                var result = await service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}