namespace TodoApp.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string IdentifierId { get; set; }
        public string Email { get; set; }
        public DateTime JoinedDate { get; set; }

        public virtual ICollection<ActionItem> ActionItems { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}