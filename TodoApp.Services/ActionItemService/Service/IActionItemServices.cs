using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.Models;
using static TodoApp.Services.Constant;

namespace TodoApp.Services.ActionItemService
{
    public interface IActionItemServices
    {
        Task<ActionResult> Add(ActionItemModel record, int userId);

        Task<ActionResult> Edit(UpdateActionItemStatus record, int userId);

        Task<ActionResult> Delete(long recordId, int userId);

        Task<List<ActionItemModel>> GetAll(int categoryId, int userId);

        Task<ActionItemList> GetAllByWidget(int categoryId, TaskWidgetType type, int skip, int take, string sortBy, string sortDirection, int userId);
    }
}