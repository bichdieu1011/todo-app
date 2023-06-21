using AutoMapper;
using TodoApp.Database.Entities;
using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.CategoryService.Model;

namespace TodoApp.Services.ActionItemService.Mapper
{
    public class ActionItemMapper : Profile
    {
        public ActionItemMapper()
        {
            CreateMap<Category, CategoryModel>();

            CreateMap<CategoryModel, Category>()
                .ForMember(s => s.Id, x => x.Ignore())
                .ForMember(s => s.IsActive, x => x.MapFrom(x => true));

        }
    }
}