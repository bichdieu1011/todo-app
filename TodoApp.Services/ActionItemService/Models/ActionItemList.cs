namespace TodoApp.Services.ActionItemService.Models
{
    public class ActionItemList
    {
        public List<ActionItemModel> Today { get; set; }
        public List<ActionItemModel> Tomorrow { get; set; }
        public List<ActionItemModel> ThisWeek { get; set; }
        public List<ActionItemModel> Expired { get; set; }
    }
}