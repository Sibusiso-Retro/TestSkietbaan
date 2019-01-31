using Backend_Skietbaan_T2.Controllers;
using Backend_Skietbaan_T2.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestSkietbaan
{


    public class UnitTest1
    {
        private UserController _userController;
        private LoginController _loginController;

        public ModelsContext getTestDatabase()
        {
            var mockData = new List<User> {
                new User{Id = 1, Username = "zintle", Surname = "Skosana", Password = "123"},
                new User{Id = 2, Username = "Mandla", Surname = "Masombuka", Password = "456"}
            }.AsQueryable();



            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

            var mockContext = new Mock<ModelsContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            return mockContext.Object;
        }

        [Fact]
        public void GetAllUsersTest()
        {
            //Act
            _userController = new UserController(getTestDatabase());
            List<User> users = _userController.Get().Result.Value.ToList();
            //Assert
            Assert.Equal(2, users.Count);

        }
        [Theory]
        [InlineData(1,"zintle", "Skosana", "123",true)]
        [InlineData(2,"Mandla", "Masombuka", "456",true)]
        public void GetSingleUserByIDTest(int id, string username,string Surname, string password,bool result)
        {
            //Act
            User expectedUser = new User();
            expectedUser.Id = id;
            expectedUser.Username = username;
            expectedUser.Surname = Surname;
            expectedUser.Password = password;

            //get user from fake DB using API controller
            _userController = new UserController(getTestDatabase());
            User user = _userController.Get(id).Result.Value;
            
            //Assert
            Assert.Equal(result, user.Id == expectedUser.Id);

        }
        [Theory]
        [InlineData("zintle", "123", "access granted")]
        [InlineData("Mandla", "456", "access granted")]
        public void TestLogin(string username,string password, string result )
        {
            //Act
            _loginController = new LoginController(getTestDatabase());
            string response = _loginController.VerifyUser(username, password).Result.Value;
            //Assert
            Assert.Equal(result, response);
        }
    }
}
