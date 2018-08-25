using Atm.Core;
using Atm.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Application.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetByIdShouldReturnUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new User
            {
                Id = 99
            });
            var userService = new UserService(null, userRepositoryMock.Object, null, null);
            var userId = 1;
            User result;

            // Act
            result = userService.GetById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(99, result.Id);
        }
    }
}