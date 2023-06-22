namespace TodoApp.Services.ActionItemService.Models
{
    public class UpdateActionItemModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}