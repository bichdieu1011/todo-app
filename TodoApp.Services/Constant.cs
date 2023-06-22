using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
