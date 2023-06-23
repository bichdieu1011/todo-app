using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.Models;
using static TodoApp.Services.Constant;

namespace TodoApp.Services.ActionItemService
{
    public interface IActionItemServices
    {
        Task<ActionResult> Add(ActionItemModel record);

        Task<ActionResult> Edit(UpdateActionItemStatus record);

        Task<ActionResult> Delete(long recordId);

        Task<List<ActionItemModel>> GetAll(int categoryId);

        Task<ActionItemList> GetAllByWidget(int categoryId, TaskWidgetType type, int skip, int take, string sortBy, string sortDirection);
    }
}