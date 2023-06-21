using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Services.ActionItemService.Models
{
    public class UpdateActionItemModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public short Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
