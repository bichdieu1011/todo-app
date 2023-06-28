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

        public ActionItemService(ToDoAppContext dbContext,
            ILogger<ActionItemService> logger,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Add(ActionItemModel record, int userId)
        {
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Result.Success,
                    Messages = new List<string>() { "Record is not exists!" }
                };
            }

            var validate = Validate(record);
            if (!string.IsNullOrEmpty(validate))
                return new ActionResult
                {
                    Result = Result.Error,
                    Messages = new List<string>() { validate }
                };

            if (userId <= 0) throw new Exception("Invalid User");

            var category = await dbContext.Set<Category>()
                .SingleOrDefaultAsync(s => s.Id == record.CategoryId && s.IsActive && s.UserId == userId);
            if (category is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            var newItem = mapper.Map<ActionItem>(record);
            newItem.UserId = userId;

            dbContext.Set<ActionItem>().Add(newItem);
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        private string Validate(ActionItemModel record)
        {
            if (string.IsNullOrWhiteSpace(record.Content))
                return "Content is required";
            if (record.Start > record.End)
                return "Start date must be less than or equal to End date";
            return string.Empty;
        }

        public async Task<ActionResult> Delete(long recordId, int userId)
        {
            if (userId <= 0) throw new Exception($"{nameof(User)} is not found");

            var record = await dbContext.Set<ActionItem>().SingleOrDefaultAsync(s => s.Id == recordId && s.UserId == userId);
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Result.Error,
                    Messages = new List<string>() { "Record is not exists" }
                };
            }

            record.Status = (short)ActionItemStatus.Removed;
            record.Updated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<ActionResult> Edit(UpdateActionItemStatus record, int userId)
        {
            if (record is null) throw new Exception($"{nameof(record)} is empty");

            if (userId <= 0) throw new Exception($"{nameof(User)} is not found");

            var item = await dbContext.Set<ActionItem>().SingleOrDefaultAsync(s => s.Id == record.Id && s.UserId == userId);
            if (item is null)
            {
                throw new Exception($"{nameof(ActionItem)} is not found");
            }

            if (!(record.NewStatus == ActionItemStatus.Open || record.NewStatus == ActionItemStatus.Done))
                return new ActionResult { Result = Constant.Result.Error, Messages = new List<string> { "Data status is invalid" } };

            if (record.CurrentStatus == record.NewStatus)
                return new ActionResult { Result = Constant.Result.Warning, Messages = new List<string> { "Data status is invalid" } };

            if (item.Status != (short)record.CurrentStatus)
                return new ActionResult { Result = Constant.Result.Warning, Messages = new List<string> { "Status is obsoleted" } };

            item.Status = (short)record.NewStatus;
            item.Updated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return new ActionResult { Result = Constant.Result.Success };
        }

        public async Task<List<ActionItemModel>> GetAll(int categoryId, int userId)
        {
            if (userId <= 0) return new List<ActionItemModel>();

            var res = await dbContext.Set<ActionItem>()
                .Where(s =>
                       (s.Status == (short)ActionItemStatus.Open
                        || s.Status == (short)ActionItemStatus.Done)
                    && s.CategoryId == categoryId
                    && s.Category.IsActive
                    && s.UserId == userId)
                .OrderByDescending(s => s.End).ThenBy(s => s.Status)
                .ToListAsync();

            return res.Select(s => mapper.Map<ActionItemModel>(s)).ToList();
        }

        public async Task<ActionItemList> GetAllByWidget(
            int categoryId, TaskWidgetType type,
            int skip, int take,
            string sortBy, string sortdirection,
            int userId)
        {
            if (userId <= 0) return new ActionItemList();

            var now = DateTime.Now;

            var today = now.ToDay();
            var tomorow = now.Tomorow();
            var thisWeek = now.ThisWeek();

            IQueryable<ActionItem> queries = dbContext.Set<ActionItem>()
                .Where(s => s.CategoryId == categoryId
                        && s.Status != (short)ActionItemStatus.Removed
                        && s.UserId == userId);

            switch (type)
            {
                case TaskWidgetType.Today:
                    queries = queries.Where(s => (s.Start <= today.Start && s.End >= today.Start));
                    break;

                case TaskWidgetType.Tomorrow:
                    queries = queries.Where(s => s.End == tomorow.Start && s.Status == (short)ActionItemStatus.Open);
                    break;

                case TaskWidgetType.ThisWeek:
                    queries = queries.Where(s => s.Start >= today.End && s.End <= thisWeek.End && s.Status == (short)ActionItemStatus.Open);
                    break;

                case TaskWidgetType.Expired:
                default:
                    queries = queries.Where(s => s.End < today.Start && s.Status == (short)ActionItemStatus.Open);
                    break;
            }

            var res = new ActionItemList();
            res.Total = await queries.CountAsync();
            queries = Sort(queries, sortBy, sortdirection);
            var items = await queries.Skip(skip).Take(take).ToListAsync();
            res.Result = items.Select(s => mapper.Map<ActionItemModel>(s)).ToList();

            return res;
        }

        private IQueryable<ActionItem> Sort(IQueryable<ActionItem> queries, string sortBy, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "status";

            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "asc";

            bool isAsc = false;
            if (string.Compare(SortDirection.asc.ToString(), sortDirection.Trim(), true) == 0)
                isAsc = true;

            switch (sortBy.ToLower())
            {
                case "content":
                    return isAsc ? queries.OrderBy(s => s.Content) : queries.OrderByDescending(s => s.Content);

                case "start":
                    return isAsc ? queries.OrderBy(s => s.Start) : queries.OrderByDescending(s => s.Start);

                case "end":
                    return isAsc ? queries.OrderBy(s => s.End) : queries.OrderByDescending(s => s.End);

                default:

                    return isAsc ? queries.OrderBy(s => s.Status) : queries.OrderByDescending(s => s.Status);
            }
        }
    }
}