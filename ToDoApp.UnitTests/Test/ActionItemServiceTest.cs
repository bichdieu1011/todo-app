using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.ActionItemService;
using TodoApp.Services.ActionItemService.Mapper;
using TodoApp.Services.ActionItemService.Models;
using static TodoApp.Services.Constant;

namespace ToDoApp.UnitTests.Test
{
    public class ActionItemServiceTest
    {
        public TestDbContextMock testDbContextMock;
        public ActionItemService actionItemService;

        public ActionItemServiceTest()
        {
            var dbOption = new DbContextOptions<ToDoAppContext>();
            testDbContextMock = new TestDbContextMock(dbOption);
            InitData();

            var logger = new Mock<ILogger<ActionItemService>>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionItemMapper());
            });
            var mapper = mappingConfig.CreateMapper();
            actionItemService = new ActionItemService(testDbContextMock, logger.Object, mapper);
        }

        [Fact]
        public async Task GetAll()
        {
            var result1 = await actionItemService.GetAll(1);
            Assert.NotEmpty(result1);

            var result2 = await actionItemService.GetAll(2);
            Assert.Empty(result2);
        }

        [Fact]
        public async Task Add_ActionItem_Success()
        {
            var record = new ActionItemModel
            {
                CategoryId = 1,
                Content = "read Database internal",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                Status = (short)ActionItemStatus.Open
            };
            var result = await actionItemService.Add(record);
            Assert.Equal(TodoApp.Services.Constant.Result.Success, result.Result);
        }

        [Fact]
        public async Task Add_ActionItem_To_NotExists_Category()
        {
            var record = new ActionItemModel
            {
                CategoryId = 3,
                Content = "do chores",
                Start = DateTime.Now.AddDays(-5),
                End = DateTime.Now
            };
            Assert.Throws<AggregateException>(() => actionItemService.Add(record).Result);
        }

        [Fact]
        public async Task Add_ActionItem_To_InActive_Category()
        {
            var record = new ActionItemModel
            {
                CategoryId = 2,
                Content = "do chores",
                Start = DateTime.Now.AddDays(-5),
                End = DateTime.Now
            };
            Assert.Throws<AggregateException>(() => actionItemService.Add(record).Result);

        }

        private void InitData()
        {
            testDbContextMock.Set<Category>().AddRange(new Category[]
            {
                new Category { Id = 1, Name = "category 1", IsActive = true}
            });

            testDbContextMock.Set<ActionItem>().AddRange(new ActionItem[]
            {
                new ActionItem {
                    Id = 1,
                    CategoryId =1,
                    Content = "read system design interview",
                    Start = DateTime.Now.AddDays(-5),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Open
                },
                new ActionItem {
                    Id = 2,
                    CategoryId =1,
                    Content = "do 100 leetcode problems (medium)",
                    Start = DateTime.Now.AddDays(-3),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Done
                }
            });

            testDbContextMock.SaveChanges();
        }
    }
}