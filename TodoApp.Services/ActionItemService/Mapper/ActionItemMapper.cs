using AutoMapper;
using TodoApp.Database.Entities;
using TodoApp.Services.ActionItemService.Models;

namespace TodoApp.Services.ActionItemService.Mapper
{
    public class ActionItemMapper : Profile
    {
        public ActionItemMapper()
        {
            CreateMap<ActionItem, ActionItemModel>()
                .ForMember(s => s.IsDone, x => x.MapFrom(x => x.Status == (short)Constant.ActionItemStatus.Done));

            CreateMap<ActionItemModel, ActionItem>()
                .ForMember(s => s.Id, x => x.Ignore())
                .ForMember(s => s.Status, x => x.Ignore())
                .ForMember(s => s.Start, x => x.MapFrom(m => m.Start.Date))
                .ForMember(s => s.End, x => x.MapFrom(m => m.End.Date))
                .ForMember(s => s.Created, x => x.MapFrom(s => DateTime.Now))
                .ForMember(s => s.Updated, x => x.MapFrom(s => DateTime.Now));

            CreateMap<UpdateActionItemModel, ActionItem>()
                 .ForMember(s => s.Id, x => x.Ignore())
                 .ForMember(s => s.Status, x => x.Ignore())
                 .ForMember(s => s.Updated, x => x.MapFrom(s => DateTime.Now))
                 .ForMember(s => s.Created, x => x.UseDestinationValue());
        }
    }
}