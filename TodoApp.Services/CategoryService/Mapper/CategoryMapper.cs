using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Database.Entities;
using TodoApp.Services.CategoryService.Model;

namespace TodoApp.Services.CategoryService.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryModel>();

            CreateMap<CategoryModel, Category>()
                .ForMember(s => s.Id, x => x.Ignore())
                .ForMember(s => s.IsActive, x => x.MapFrom(x => true));

        }
    }
}
