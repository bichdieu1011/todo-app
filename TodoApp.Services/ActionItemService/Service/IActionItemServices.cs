using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.Models;
using static TodoApp.Services.Constant;

namespace TodoApp.Services.ActionItemService
{
    public interface IActionItemServices
    {
        Task<ActionResult> Add(ActionItemModel record, string email);

        Task<ActionResult> Edit(UpdateActionItemStatus record, string email);

        Task<ActionResult> Delete(long recordId, string email);

        Task<List<ActionItemModel>> GetAll(int categoryId, string email);

        Task<ActionItemList> GetAllByWidget(int categoryId, TaskWidgetType type, int skip, int take, string sortBy, string sortDirection, string email);
    }
}