using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TodoApp.Services.Constant;
namespace TodoApp.Services.Models
{
    public class ActionResult
    {
        public Result  Result { get; set; }
        public List<string> Messages { get; set; }
    }
}
