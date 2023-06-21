using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.ActionItemService.Models;
using TodoApp.Services.Models;
using static TodoApp.Services.Constant;

namespace TodoApp.Services.ActionItemService
{
    public class ActionItemService : IActionItemServices
    {
        private readonly ToDoAppContext dbContext;
        private readonly ILogger<ActionItemService> logger;
        private readonly IMapper mapper;

        public ActionItemService(ToDoAppContext dbContext, ILogger<ActionItemService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Add(ActionItemModel record)
        {
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Constant.Result.Success,
                    Messages = new List<string>() { "Record is not exists!" }
                };
            }

            var category = await dbContext.Set<Category>()
                .SingleOrDefaultAsync(s => s.Id == record.CategoryId && s.IsActive);
            if (category is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            var newItem = mapper.Map<ActionItem>(record);
            dbContext.Set<ActionItem>().Add(newItem);
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Constant.Result.Success
            };
        }

        public async Task<ActionResult> Delete(long recordId)
        {
            var record = await dbContext.Set<ActionItem>().SingleOrDefaultAsync(s => s.Id == recordId);
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Constant.Result.Success,
                    Messages = new List<string>() { "Record is not exists" }
                };
            }
            dbContext.Set<ActionItem>().Remove(record);
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Constant.Result.Success
            };
        }

        public async Task<ActionResult> Edit(UpdateActionItemModel record)
        {
            if (record is null)
                throw new Exception("record is empty");

            var item = await dbContext.Set<ActionItem>().SingleOrDefaultAsync(s => s.Id == record.Id);
            if (record is null)
            {
                throw new Exception($"{nameof(ActionItem)} is not found");
            }

            mapper.Map(record, item);
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Constant.Result.Success
            };
        }

        public async Task<List<ActionItemModel>> GetAll(int categoryId)
        {
            var res = await dbContext.Set<ActionItem>()
                .Where(s => 
                       (s.Status == (short)ActionItemStatus.Open 
                        || s.Status == (short)ActionItemStatus.Done)
                    && s.CategoryId == categoryId 
                    && s.Category.IsActive)
                .OrderByDescending(s => s.End).ThenBy(s => s.Status)
                .ToListAsync();

            return res.Select(s => mapper.Map<ActionItemModel>(s)).ToList();
        }

        public async Task<ActionItemList> GetAllByWidget(int categoryId)
        {
            var now = DateTime.Now;
            var today = now.ToDay();
            var tomorow = now.Tomorow();
            var thisWeek = now.ThisWeek();

            var actionItemOfToday = await dbContext.Set<ActionItem>().Where(s => s.CategoryId == categoryId && s.Start <= today.Start && s.End >= today.End).OrderBy(s => s.Status).ToListAsync();
            var actionItemOfTomorow = await dbContext.Set<ActionItem>().Where(s => s.CategoryId == categoryId && s.Start <= tomorow.Start && s.End >= tomorow.End && s.Status == (short)ActionItemStatus.Open).OrderBy(s => s.Status).Take(10).ToListAsync();
            var actionItemOfThisWeek = await dbContext.Set<ActionItem>().Where(s => s.CategoryId == categoryId && s.Start <= thisWeek.Start && s.End >= thisWeek.End && s.Status == (short)ActionItemStatus.Open).OrderBy(s => s.Status).Take(10).ToListAsync();
            var actionItemOfExpired = await dbContext.Set<ActionItem>().Where(s => s.CategoryId == categoryId && s.End <= today.Start && s.Status == (short)ActionItemStatus.Open).OrderBy(s => s.Status).Take(10).ToListAsync();

            var res = new ActionItemList()
            {
                Today = actionItemOfToday.Select(s => mapper.Map<ActionItemModel>(s)).ToList(),
                Tomorrow = actionItemOfTomorow.Select(s => mapper.Map<ActionItemModel>(s)).ToList(),
                ThisWeek = actionItemOfThisWeek.Select(s => mapper.Map<ActionItemModel>(s)).ToList(),
                Expired = actionItemOfExpired.Select(s => mapper.Map<ActionItemModel>(s)).ToList()
            };

            return res;
        }
    }
}