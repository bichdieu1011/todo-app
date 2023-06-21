using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.Models;
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

        public async Task<ActionResult> Activate(int id)
        {
            var item = await dbContext.Set<Category>().SingleOrDefaultAsync(s => s.Id == id);
            if (item is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            item.IsActive = true;
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<ActionResult> Add(CategoryModel record)
        {
            if (record is null)
            {
                return new ActionResult
                {
                    Result = Result.Success,
                    Messages = new List<string>() { "Record is not exists!" }
                };
            }

            var newItem = mapper.Map<Category>(record);
            dbContext.Set<Category>().Add(newItem);
            await dbContext.SaveChangesAsync();

            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<ActionResult> Deactivate(int id)
        {
            var category = await dbContext.Set<Category>().SingleOrDefaultAsync(s => s.Id == id);
            if (category is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            category.IsActive = false;

            var items = await dbContext.Set<ActionItem>().Where(s => s.CategoryId == id && s.Status == (short)ActionItemStatus.Open).ToListAsync();
            foreach (var item in items)
            {
                item.Status = (short)ActionItemStatus.Removed;
            }

            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<ActionResult> Edit(CategoryModel record)
        {
            if (record is null)
                throw new Exception("record is empty");

            var item = await dbContext.Set<Category>().SingleOrDefaultAsync(s => s.Id == record.Id);
            if (item is null)
            {
                throw new Exception($"{nameof(Category)} is not found");
            }

            mapper.Map(record, item);
            await dbContext.SaveChangesAsync();
            return new ActionResult
            {
                Result = Result.Success
            };
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            var categories = await dbContext.Set<Category>().Where(c => c.IsActive).ToListAsync();
            return categories.Select(s => mapper.Map<CategoryModel>(s)).ToList();
        }
    }
}