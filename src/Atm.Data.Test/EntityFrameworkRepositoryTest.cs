using Atm.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Atm.Data.Test
{
    public class FakeSet<T> : DbSet<T>, IQueryable where T : class
    {
        private readonly IEnumerable<T> _values;

        public FakeSet(IEnumerable<T> values)
        {
            _values = values;
        }

        IQueryProvider IQueryable.Provider => _values.AsQueryable().Provider;

        System.Linq.Expressions.Expression IQueryable.Expression => _values.AsQueryable().Expression;

        Type IQueryable.ElementType => _values.AsQueryable().ElementType;

        public IList<T> Values => _values.ToList();

        public override T Add(T entity)
        {
            (_values as List<T>)?.Add(entity);
            return entity;
        }

        public override T Remove(T entity)
        {
            (_values as List<T>)?.Remove(entity);
            return entity;
        }

        public override T Find(params object[] keyValues)
        {
            return _values.ToArray()[Convert.ToInt32(keyValues[0])];
        }

        public override T Attach(T entity)
        {            
            return entity;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateShouldAddEntity()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>();
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);
            var user = new User
            {
                Id = 3
            };

            // Act
            entityFrameworkRepository.Create(user);

            // Assert
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(3, users[0].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWithNullShouldThrowArgument()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>();
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);

            // Act
            entityFrameworkRepository.Create(null);
        }

        [TestMethod]
        public void GetByIdShouldCallFind()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>
            {
                new User
                {
                    UserName = "samus"
                },
                new User
                {
                    UserName = "link"
                },
                new User
                {
                    UserName = "mario"
                }
            };
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);
            User result;

            // Act
            result = entityFrameworkRepository.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("link", result.UserName);
        }

        [TestMethod]
        public void DeleteShouldRemoveEntity()
        {
            // Arrange
            var context = new Mock<DbContext>();
            User user = new User
            {
                UserName = "samus"
            };
            var users = new List<User>
            {                
                new User
                {
                    UserName = "link"
                },
                user,
                new User
                {
                    UserName = "mario"
                }
            };
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);
            
            // Act
            entityFrameworkRepository.Delete(user);

            // Assert
            Assert.AreEqual(2, users.Count);
            Assert.IsFalse(users.Contains(user));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWithNullShouldThrowArgumentNull()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>();
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);

            // Act
            entityFrameworkRepository.Delete(null);
        }

        //[TestMethod]
        //public void UpdateShouldChangeEntityState()
        //{
        //    // Arrange
        //    var context = new Mock<DbContext>();
        //    User user = new User
        //    {
        //        UserName = "samus"
        //    };
        //    var users = new List<User>();
        //    context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
        //    var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);
        //    var dbentityEntry = new Mock<DbEntityEntry>();
        //    context.Setup(m => m.Entry(It.IsAny<Object>())).Returns(dbentityEntry.Object);

        //    // Act
        //    entityFrameworkRepository.Update(user);

        //    // Assert
        //    dbentityEntry.Verify(m => m.State, Times.Once);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldChangeEntityState()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var users = new List<User>();
            context.Setup(m => m.Set<User>()).Returns(new FakeSet<User>(users));
            var entityFrameworkRepository = new EntityFrameworkRepository<User, int>(context.Object);

            // Act
            entityFrameworkRepository.Update(null);
        }
    }
}