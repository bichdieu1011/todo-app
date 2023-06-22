using static TodoApp.Services.Constant;

namespace TodoApp.Services.ActionItemService.Models
{
    public class UpdateActionItemStatus
    {
        public long Id { get; set; }
        public ActionItemStatus CurrentStatus { get; init; }
        public ActionItemStatus NewStatus { get; init; }
    }
}