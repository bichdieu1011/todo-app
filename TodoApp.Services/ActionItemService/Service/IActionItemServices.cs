using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.Models;

namespace TodoApp.Services.ActionItemService
{
    public interface IActionItemServices
    {
        Task<ActionResult> Add(ActionItemModel record);

        Task<ActionResult> Edit(UpdateActionItemModel record);

        Task<ActionResult> Delete(long recordId);

        Task<List<ActionItemModel>> GetAll(int categoryId);

        Task<ActionItemList> GetAllByWidget(int categoryId);
    }
}