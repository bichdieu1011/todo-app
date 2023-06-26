using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.UserService.Service;

namespace ToDoApp.UnitTests.Test
{
    public class UserServiceTest
    {
        public TestDbContextMock testDbContextMock;
        public UserService userService;
        public Mock<IMemoryCache> cacheMemory;

        public UserServiceTest()
        {
            var dbOption = new DbContextOptions<ToDoAppContext>();
            testDbContextMock = new TestDbContextMock(dbOption);
            InitData();

            var logger = new Mock<ILogger<UserService>>();
            cacheMemory = new Mock<IMemoryCache>();

            userService = new UserService(logger.Object, cacheMemory.Object, testDbContextMock);
        }

        [Fact]
        public async Task Get_Existed_User_By_Email()
        {
            cacheMemory.Setup(s => s.CreateEntry($"_useremail:email")).Returns(Mock.Of<ICacheEntry>().SetValue(1));

            var userId = await userService.GetUserIdByEmail("email");
            Assert.Equal(1, userId);
        }

        [Fact]
        public async Task Get_NoneExisted_User_By_Email()
        {
            var userId = await userService.GetUserIdByEmail("email1");
            Assert.Equal(0, userId);
        }

        [Fact]
        public async Task GetOrAddExistedUser_Cached()
        {
            cacheMemory.Setup(s => s.CreateEntry($"_useremail:email")).Returns(Mock.Of<ICacheEntry>().SetValue(1));
            var userId = await userService.GetOrAddUser("email");
            Assert.Equal(1, userId);
        }

        [Fact]
        public async Task GetOrAddExistedUser_NoneCached()
        {
            cacheMemory.Setup(s => s.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);

            var userId = await userService.GetOrAddUser("email");
            Assert.Equal(1, userId);
        }

        [Fact]
        public async Task GetOrAddNoneExistedUser()
        {
            cacheMemory.Setup(s => s.CreateEntry(It.IsAny<object>()))
               .Returns(Mock.Of<ICacheEntry>);
            var userId = await userService.GetOrAddUser("email2");
            Assert.Equal(2, userId);
        }

        [Fact]
        public async Task AddExisedUser()
        {
            var user = await userService.AddUser(new User { Email = "email" });
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public async Task AddNoneExisedUser()
        {
            var user = await userService.AddUser(new User { Email = "email2" });
            Assert.NotNull(user);
            Assert.Equal(2, user.Id);
            Assert.Equal(DateTime.Today, user.JoinedDate.Date);
        }

        private void InitData()
        {
            testDbContextMock.Set<User>().AddRange(new User[]
            {
                new User { Id = 1, Email = "email", JoinedDate = DateTime.Now}
            });
            testDbContextMock.SaveChanges();
        }
    }
}