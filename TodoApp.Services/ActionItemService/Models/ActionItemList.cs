namespace TodoApp.Services.ActionItemService.Models
{
    public class ActionItemList
    {
        public List<ActionItemModel> Result { get; set; } = new List<ActionItemModel>();
        public int Total { get; set; }
    }
}