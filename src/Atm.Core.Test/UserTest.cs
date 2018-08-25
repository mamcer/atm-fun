using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atm.Core.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void ConstructorShouldInitializeAccounts()
        {
            // Arrange
            User user;

            // Act 
            user = new User();

            // Assert
            Assert.IsNotNull(user.Accounts);
            Assert.AreEqual(0, user.Accounts.Count);
        }
    }
}
