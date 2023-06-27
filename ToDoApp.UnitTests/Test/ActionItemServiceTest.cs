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
        public async Task GetAllByWidget_ToDay()
        {
            var result1 = await actionItemService.GetAllByWidget(1, TaskWidgetType.Today, 0, 5, "content", "asc", 1);
            Assert.NotNull(result1);
            Assert.Equal(2, result1.Total);
        }

        public async Task UnAuthorisedUser_GetAllByWidget_ToDay()
        {
            var result1 = await actionItemService.GetAllByWidget(1, TaskWidgetType.Today, 0, 5, "content", "asc", -1);
            Assert.NotNull(result1);
            Assert.Equal(0, result1.Total);
        }

        [Fact]
        public async Task GetAll()
        {
            var result1 = await actionItemService.GetAll(1, 1);
            Assert.NotEmpty(result1);

            var result2 = await actionItemService.GetAll(2, 1);
            Assert.Empty(result2);
        }

        [Fact]
        public async Task UnAuthorisedUser_GetAll()
        {
            var result1 = await actionItemService.GetAll(1, 2);
            Assert.Empty(result1);

            var result2 = await actionItemService.GetAll(2, -1);
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
            var result = await actionItemService.Add(record, 1);
            Assert.Equal(Result.Success, result.Result);
        }

        [Fact]
        public async Task UnAuthorisedUser_Add_ActionItem()
        {
            var record = new ActionItemModel
            {
                CategoryId = 1,
                Content = "read Database internal",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                Status = (short)ActionItemStatus.Open
            };
            await Assert.ThrowsAsync<Exception>(() => actionItemService.Add(record, 2));
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
            await Assert.ThrowsAsync<Exception>(() => actionItemService.Add(record, 1));
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
            await Assert.ThrowsAsync<Exception>(() => actionItemService.Add(record, 1));
        }

        [Fact]
        public async Task UnAuthorisedUser_Edit_ActionItem_Valid_Status()
        {
            var record = new UpdateActionItemStatus
            {
                Id = 1,
                CurrentStatus = ActionItemStatus.Open,
                NewStatus = ActionItemStatus.Done
            };

            await Assert.ThrowsAsync<Exception>(() => actionItemService.Edit(record, 2));
        }

        [Fact]
        public async Task Edit_ActionItem_Valid_Status()
        {
            var record = new UpdateActionItemStatus
            {
                Id = 1,
                CurrentStatus = ActionItemStatus.Open,
                NewStatus = ActionItemStatus.Done
            };

            var res = await actionItemService.Edit(record, 1);
            Assert.Equal(Result.Success, res.Result);
        }

        [Fact]
        public async Task Edit_No_Exists_ActionItem()
        {
            var record = new UpdateActionItemStatus
            {
                Id = 0,
                CurrentStatus = ActionItemStatus.Open,
                NewStatus = ActionItemStatus.Done
            };
            await Assert.ThrowsAsync<Exception>(() => actionItemService.Edit(record, 1));
        }

        [Fact]
        public async Task Edit_ActionItem_Obsolete_Status()
        {
            var record = new UpdateActionItemStatus
            {
                Id = 1,
                CurrentStatus = ActionItemStatus.Done,
                NewStatus = ActionItemStatus.Open
            };

            var res = await actionItemService.Edit(record, 1);
            Assert.Equal(Result.Warning, res.Result);
        }

        [Fact]
        public async Task Edit_ActionItem_Invalid_Status()
        {
            var record = new UpdateActionItemStatus
            {
                Id = 1,
                CurrentStatus = ActionItemStatus.Open,
                NewStatus = ActionItemStatus.Removed
            };

            var res = await actionItemService.Edit(record, 1);
            Assert.Equal(Result.Error, res.Result);
        }

        [Fact]
        public async Task Delete_ActionItem()
        {
            var res = await actionItemService.Delete(1, 1);
            Assert.Equal(Result.Success, res.Result);
        }

        [Fact]
        public async Task InvalidUser_Delete_ActionItem()
        {
            await Assert.ThrowsAsync<Exception>(() => actionItemService.Delete(1, -1));
        }


        [Fact]
        public async Task UnAuthorisedUser_Delete_ActionItem()
        {
            var res = await actionItemService.Delete(1, 2);
            Assert.Equal(Result.Error, res.Result);
        }

        [Fact]
        public async Task Delete_No_Exists_ActionItem()
        {
            var res = await actionItemService.Delete(0, 1);
            Assert.Equal(Result.Error, res.Result);
        }

        private void InitData()
        {
            testDbContextMock.Set<Category>().AddRange(new Category[]
            {
                new Category { Id = 1, Name = "category 1", IsActive = true, UserId = 1}
            });

            testDbContextMock.Set<ActionItem>().AddRange(new ActionItem[]
            {
                new ActionItem {
                    Id = 1,
                    CategoryId =1,
                    Content = "read system design interview",
                    Start = DateTime.Now.AddDays(-5),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Open,
                    UserId = 1
                },
                new ActionItem {
                    Id = 2,
                    CategoryId =1,
                    Content = "do 100 leetcode problems (medium)",
                    Start = DateTime.Now.AddDays(-3),
                    End = DateTime.Now,
                    Status = (short)ActionItemStatus.Done,
                    UserId = 1
                }
            });

            testDbContextMock.Set<User>().AddRange(new User[]
            {
                new User { Id = 1, Email = "email", IdentifierId="id1"}
            });

            testDbContextMock.SaveChanges();
        }
    }
}