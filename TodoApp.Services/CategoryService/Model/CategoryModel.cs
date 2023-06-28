using System.ComponentModel.DataAnnotations;

namespace TodoApp.Services.CategoryService.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}