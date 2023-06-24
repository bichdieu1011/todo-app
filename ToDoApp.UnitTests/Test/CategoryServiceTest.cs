using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.CategoryService.Mapper;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.CategoryService.Service;

namespace ToDoApp.UnitTests.Test
{
    public class CategoryServiceTest
    {
        public TestDbContextMock testDbContextMock;
        public CategoryService categoryService;

        public CategoryServiceTest()
        {
            var dbOption = new DbContextOptions<ToDoAppContext>();
            testDbContextMock = new TestDbContextMock(dbOption);
            InitData();

            var logger = new Mock<ILogger<CategoryService>>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryMapper());
            });
            var mapper = mappingConfig.CreateMapper();
            categoryService = new CategoryService(testDbContextMock, logger.Object, mapper);
        }

        [Fact]
        public async Task Get()
        {
            var result = await categoryService.GetAll();
            Assert.Single(result);
        }

        [Fact]
        public async Task Add_Category_Success()
        {
            var record = new CategoryModel { Name = "category test" };
            var result = await categoryService.Add(record);
            Assert.Equal(TodoApp.Services.Constant.Result.Success, result.Result);
        }

        [Fact]
        public async Task Add_Duplicated_Active_Category_HasError()
        {
            var record = new CategoryModel { Name = "category 1" };
            var result = await categoryService.Add(record);
            Assert.Equal(TodoApp.Services.Constant.Result.Error, result.Result);
        }

        [Fact]
        public async Task Add_Duplicated_Inactive_Category()
        {
            var record = new CategoryModel { Name = "category 2" };
            var result = await categoryService.Add(record);
            Assert.Equal(TodoApp.Services.Constant.Result.Success, result.Result);
        }

        [Fact]
        public async Task Get_All_Category()
        {
            var result = await categoryService.GetAll();
            Assert.Single(result);
            Assert.Contains(result, item => item.Id == 1);
            Assert.DoesNotContain(result, item => item.Id == 2);
        }

        [Fact]
        public async Task Deactivate_An_Active_Category()
        {
            testDbContextMock.Set<Category>().AddRange(new Category[]
            {
                new Category {
                    Id = 3,
                    Name = "category 3",
                    IsActive = true,
                    ActionItems = new List<ActionItem>{
                            new ActionItem
                            {
                                Id = 1,
                                CategoryId = 3,
                                Content = "item 1",
                                Status = 0
                            }
                    }
                }
            });
            testDbContextMock.SaveChanges();

            var result = await categoryService.Deactivate(3);
            Assert.Equal(TodoApp.Services.Constant.Result.Success, result.Result);
            
            var category = testDbContextMock.Set<Category>().Include(x => x.ActionItems).SingleOrDefault(s => s.Id ==3);
            Assert.NotNull(category);
            Assert.False(category.IsActive);
            Assert.DoesNotContain(category.ActionItems, s =>  s.Status != (short)TodoApp.Services.Constant.ActionItemStatus.Removed );
        }

        private void InitData()
        {
            testDbContextMock.Set<Category>().AddRange(new Category[]
            {
                new Category { Id = 1, Name = "category 1", IsActive = true},
                new Category { Id = 2, Name = "category 2", IsActive = false}
            });
            testDbContextMock.SaveChanges();
        }
    }
}