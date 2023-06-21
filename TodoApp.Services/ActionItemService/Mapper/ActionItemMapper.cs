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
            CreateMap<ActionItem, ActionItemModel>();

            CreateMap<ActionItemModel, ActionItem>()
                .ForMember(s => s.Id, x => x.Ignore())
                .ForMember(s => s.Status, x => x.Ignore());

            CreateMap<UpdateActionItemModel, ActionItem>()
                 .ForMember(s => s.Id, x => x.Ignore())
                 .ForMember(s => s.Status, x => x.Ignore());

        }
    }
}