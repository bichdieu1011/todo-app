namespace TodoApp.Services
{
    public static class Constant
    {
        public enum ActionItemStatus
        {
            Open,
            Done,
            Removed
        }

        public enum Result
        {
            Success,
            Warning,
            Error
        }

        public enum TaskWidgetType
        {
            Today,
            Tomorrow,
            ThisWeek,
            Expired
        }

        public enum SortDirection
        {
            asc,
            desc
        }
    }
}