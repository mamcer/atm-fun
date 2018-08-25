using System.Collections.Generic;
using System.Data.Entity;
using Atm.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Data.Test
{
    [TestClass]
    public class EntityFrameworkUnitOfWorkTest
    {
        [TestMethod]
        public void SaveChangesShouldCallDbContextSaveChanges()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>();
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkUnitOfWork = new EntityFrameworkUnitOfWork(context.Object);

            // Act
            entityFrameworkUnitOfWork.SaveChanges();

            // Assert
            context.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
