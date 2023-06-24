using static TodoApp.Services.Constant;

namespace TodoApp.Services.Models
{
    public class ActionResult
    {
        public Result Result { get; set; }
        public List<string> Messages { get; set; }
    }
}