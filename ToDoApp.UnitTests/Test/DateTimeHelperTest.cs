using TodoApp.Services;

namespace ToDoApp.UnitTests.Test
{
    public class DateTimeHelperTest
    {
        public DateTimeHelperTest()
        { }

        [Fact]
        public void TestToday()
        {
            var today = DateTime.Now;
            var res = today.ToDay();
            Assert.Equal(today.Day, res.Start.Day);
            Assert.Equal(today.Day, res.End.Day);

            Assert.Equal(today.Month, res.Start.Month);
            Assert.Equal(today.Month, res.End.Month);

            Assert.Equal(today.Year, res.Start.Year);
            Assert.Equal(today.Year, res.End.Year);

            Assert.Equal(0, res.Start.Hour);
            Assert.Equal(0, res.Start.Minute);
            Assert.Equal(0, res.Start.Second);
            Assert.Equal(23, res.End.Hour);
            Assert.Equal(59, res.End.Minute);
            Assert.Equal(59, res.End.Second);
        }

        [Fact]
        public void TestTomorrow()
        {
            var today = new DateTime(2023, 01, 01);
            var res = today.Tomorow();
            Assert.Equal(today.Day + 1, res.Start.Day);
            Assert.Equal(today.Day + 1, res.End.Day);

            Assert.Equal(today.Month, res.Start.Month);
            Assert.Equal(today.Month, res.End.Month);

            Assert.Equal(today.Year, res.Start.Year);
            Assert.Equal(today.Year, res.End.Year);

            Assert.Equal(0, res.Start.Hour);
            Assert.Equal(0, res.Start.Minute);
            Assert.Equal(0, res.Start.Second);
            Assert.Equal(23, res.End.Hour);
            Assert.Equal(59, res.End.Minute);
            Assert.Equal(59, res.End.Second);
        }

        [Fact]
        public void TestThisWeek()
        {
            var today = new DateTime(2023, 01, 03);
            var res = today.ThisWeek();
            Assert.Equal(1, res.Start.Day);
            Assert.Equal(7, res.End.Day);

            Assert.Equal(today.Month, res.Start.Month);
            Assert.Equal(today.Month, res.End.Month);

            Assert.Equal(today.Year, res.Start.Year);
            Assert.Equal(today.Year, res.End.Year);

            Assert.Equal(0, res.Start.Hour);
            Assert.Equal(0, res.Start.Minute);
            Assert.Equal(0, res.Start.Second);
            Assert.Equal(23, res.End.Hour);
            Assert.Equal(59, res.End.Minute);
            Assert.Equal(59, res.End.Second);
        }
    }
}