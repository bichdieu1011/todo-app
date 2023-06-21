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