using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;
using TodoApp.Services.UserService.Service;
using static TodoApp.Services.Constant;

namespace TodoApp.Services.CategoryService.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ToDoAppContext dbContext;
        private readonly ILogger<CategoryService> logger;
        private readonly IMapper mapper;

        public CategoryService(ToDoAppContext dbContext, ILogger<CategoryService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Add(CategoryModel record, int userId)
        {
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Result.Error,
                    Messages = new List<string>() { "Record is not exists!" }
                };
            }

            if (string.IsNullOrWhiteSpace(record.Name))
                return new ActionResult
                {
                    Result = Result.Error,
                    Messages = new List<string>() { "Name is required" }
                };

            if (userId <= 0) throw new Exception("User is invalid");

            var checkDuplicated = dbContext.Set<Category>()
                .Where(s => s.Name == record.Name && s.UserId == userId && s.IsActive)
                .Any();
            if (checkDuplicated)
            {
                return new ActionResult
                {
                    Result = Result.Error,
                    Messages = new List<string>() { "Duplicated Category!" }
                };
            }

            var newItem = mapper.Map<Category>(record);
            newItem.UserId = userId;
            dbContext.Set<Category>().Add(newItem);
            await dbContext.SaveChangesAsync();

            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<ActionResult> Deactivate(int id, int userId)
        {
            if (userId <= 0) throw new Exception($"{nameof(User)} is not found");

            var category = await dbContext.Set<Category>().SingleOrDefaultAsync(s => s.Id == id && s.UserId == userId);
            if (category is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            category.IsActive = false;
            category.Updated = DateTime.Now;

            var items = await dbContext.Set<ActionItem>()
                .Where(s => s.CategoryId == id
                    && s.Status == (short)ActionItemStatus.Open
                    && s.UserId == userId)
                .ToListAsync();

            foreach (var item in items)
            {
                item.Status = (short)ActionItemStatus.Removed;
                item.Updated = DateTime.Now;
            }

            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<List<CategoryModel>> GetAll(int userId)
        {
            if (userId <= 0) return new List<CategoryModel>();

            var categories = await dbContext.Set<Category>().Where(c => c.IsActive && c.UserId == userId).ToListAsync();
            return categories.Select(s => mapper.Map<CategoryModel>(s)).ToList();
        }
    }
}