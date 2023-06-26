namespace TodoApp.Database.Entities
{
    public class ActionItem
    {
        public long Id { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public short Status { get; set; }
        public int UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }
}