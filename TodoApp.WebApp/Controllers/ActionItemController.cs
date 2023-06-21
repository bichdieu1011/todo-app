using Microsoft.AspNetCore.Mvc;
using TodoApp.Services.ActionItemService;
using TodoApp.Services.ActionItemService.Models;

namespace TodoApp.WebApp.Controllers
{
    [Route("actionItem")]
    [ApiController]
    public class ActionItemController : Controller
    {
        private readonly ILogger<ActionItemController> logger;
        private readonly IActionItemServices service;

        public ActionItemController(ILogger<ActionItemController> logger, IActionItemServices service)
        {
            this.logger = logger;
            this.service = service;
        }

        [Route("all/{categoryId:int}")]
        [HttpGet]
        public async Task<ActionResult> GetAllActionItemAsync(int categoryId)
        {
            try
            {
                var result = await service.GetAll(categoryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("widget/{categoryId:int}")]
        [HttpGet]
        public async Task<ActionResult> GetAllActionItemByWidgetAsync(int categoryId)
        {
            try
            {
                var result = await service.GetAllByWidget(categoryId);
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

        [Route("edit/{id:long}")]
        [HttpPut]
        public async Task<ActionResult> EditActionItemAsync(long id, [FromBody] UpdateActionItemModel model)
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

        [Route("delete/{id:long}")]
        [HttpPut]
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