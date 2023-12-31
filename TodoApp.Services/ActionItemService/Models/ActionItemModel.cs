﻿using System.ComponentModel.DataAnnotations;

namespace TodoApp.Services.ActionItemService.Models
{
    public class ActionItemModel
    {
        public long Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        public bool IsDone { get; set; }

        public short Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}