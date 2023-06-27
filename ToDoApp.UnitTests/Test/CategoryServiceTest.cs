using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.CategoryService.Mapper;
using TodoApp.Services.CategoryService.Model;
using TodoApp.Services.CategoryService.Service;
using TodoApp.Services.UserService.Service;
using static TodoApp.Services.Constant;

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
            var result = await categoryService.GetAll(1);
            Assert.Contains(result, s => s.Name == "category 1");
        }

        [Fact]
        public async Task UnauthorisedUser_Get()
        {
            var result = await categoryService.GetAll(2);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Add_Category_Success()
        {
            var record = new CategoryModel { Name = "category test" };
            var result = await categoryService.Add(record, 1);
            Assert.Equal(Result.Success, result.Result);
        }

        [Fact]
        public async Task InvalidUser_Add_Category()
        {
            var record = new CategoryModel { Name = "category test" };
            await Assert.ThrowsAsync<Exception>(() => categoryService.Add(record, -1));
        }

        [Fact]
        public async Task Add_Duplicated_Active_Category_HasError()
        {
            var record = new CategoryModel { Name = "category 1" };
            var result = await categoryService.Add(record,1);
            Assert.Equal(Result.Error, result.Result);
        }

        [Fact]
        public async Task Add_Duplicated_Inactive_Category()
        {
            var record = new CategoryModel { Name = "category 2" };
            var result = await categoryService.Add(record, 1);
            Assert.Equal(Result.Success, result.Result);
        }

        [Fact]
        public async Task Get_All_Category()
        {
            var result = await categoryService.GetAll(1);
            Assert.Contains(result, s => s.Name == "category 1");
            Assert.Contains(result, item => item.Id == 1);
            Assert.DoesNotContain(result, item => item.Id == 2);
        }

        [Fact]
        public async Task UnauthorisedUser_Get_All_Category()
        {
            var result = await categoryService.GetAll(2);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Deactivate_An_Active_Category()
        {
            var result = await categoryService.Deactivate(3, 1);
            Assert.Equal(Result.Success, result.Result);

            var category = testDbContextMock.Set<Category>().Include(x => x.ActionItems).SingleOrDefault(s => s.Id == 3);
            Assert.NotNull(category);
            Assert.False(category.IsActive);
            Assert.DoesNotContain(category.ActionItems, s => s.Status == (short)ActionItemStatus.Open);
        }

        [Fact]
        public async Task UnauthorisedUser_Deactivate_An_Category()
        {
            await Assert.ThrowsAsync<Exception>(() => categoryService.Deactivate(3, -1));
            await Assert.ThrowsAsync<Exception>(() => categoryService.Deactivate(3, 2));
        }

        private void InitData()
        {
            testDbContextMock.Set<Category>().AddRange(new Category[]
            {
                new Category { Id = 1, Name = "category 1", IsActive = true, UserId = 1},
                new Category { Id = 2, Name = "category 2", IsActive = false, UserId = 1},
                new Category { Id = 3, Name = "category 3", IsActive = true, UserId = 1}
            });

            testDbContextMock.Set<ActionItem>().AddRange(new ActionItem[]
            {
                new ActionItem {
                    Id = 1,
                    CategoryId = 3,
                    Content = "read system design interview",
                    Start = DateTime.Now.AddDays(-5),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Open,
                    UserId = 1
                },
                new ActionItem {
                    Id = 2,
                    CategoryId = 3,
                    Content = "do 100 leetcode problems (medium)",
                    Start = DateTime.Now.AddDays(-3),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Done,
                    UserId = 1
                }
            });

            testDbContextMock.Set<User>().AddRange(new User[]
            {
                new User { Id = 1, Email = "email",IdentifierId="id1" },
                new User { Id = 2, Email = "email2",IdentifierId="id2"}
            });
            testDbContextMock.SaveChanges();
        }
    }
}