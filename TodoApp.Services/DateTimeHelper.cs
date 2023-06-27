namespace TodoApp.Services
{
    public struct DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public static class DateTimeHelper
    {
        public struct DateRange
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public static DateRange ToDay(this DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = date.Date;
            range.End = range.Start.AddHours(23).AddMinutes(59).AddSeconds(59);

            return range;
        }

        public static DateRange Tomorow(this DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = date.Date.AddDays(1);
            range.End = range.Start.AddHours(23).AddMinutes(59).AddSeconds(59);

            return range;
        }

        public static DateRange ThisWeek(this DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = date.Date.AddDays(-(int)date.DayOfWeek);
            range.End = range.Start.AddDays(7).AddSeconds(-1);

            return range;
        }
    }
}