using System;
using Atm.Core;
using Atm.Data;
using CrossCutting.Core.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Application.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HasEnoughFundsShouldReturnTrueWhenZero()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account
            {
                Amount = 10
            });
            var accountId = 1;
            var amount = 10;
            var accountService = new AccountService(null, accountRepositoryMock.Object, null, null);
            bool result;

            // Act
            result = accountService.HasEnoughFunds(accountId, amount);        

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasEnoughFundsShouldReturnFalseWhenLessThanZero()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account
            {
                Amount = 10
            });
            var accountId = 1;
            var amount = 11;
            var accountService = new AccountService(null, accountRepositoryMock.Object, null, null);
            bool result;

            // Act
            result = accountService.HasEnoughFunds(accountId, amount);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasEnoughFundsShouldReturnTrueWhenHighThanZero()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account
            {
                Amount = 10
            });
            var accountId = 1;
            var amount = 9;
            var accountService = new AccountService(null, accountRepositoryMock.Object, null, null);
            bool result;

            // Act
            result = accountService.HasEnoughFunds(accountId, amount);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WithdrawShouldUpdateAmount()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var operationJournalMock = new Mock<IOperationJournalService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account
            {
                Amount = 100,
                User = new User
                {
                    AtmCard = new AtmCard
                    {
                        Id = 1
                    }
                }
            });
            accountRepositoryMock.Setup(m => m.Update(It.IsAny<Account>()));
            operationJournalMock.Setup(m =>
                m.LogOperation(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OperationCode>()));
            unitOfWorkMock.Setup(m => m.SaveChanges());

            var accountId = 1;
            var amount = 70;
            var accountService = new AccountService(unitOfWorkMock.Object, accountRepositoryMock.Object, operationJournalMock.Object, null);
            Account account;

            // Act
            account = accountService.Withdraw(accountId, amount);

            // Assert
            Assert.AreEqual(30, account.Amount);
            operationJournalMock.Verify(x => x.LogOperation(It.IsAny<int>(), It.IsAny<int>(), OperationCode.Withdrawal), Times.Once);
            accountRepositoryMock.Verify(m => m.Update(It.IsAny<Account>()), Times.Once);
            unitOfWorkMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void WithdrawOnExceptionShouldReturnNull()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(m => m.Error(It.IsAny<string>()));
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Throws<InvalidOperationException>();

            var accountId = 1;
            var amount = 70;
            var accountService = new AccountService(null, accountRepositoryMock.Object, null, logServiceMock.Object);
            Account account;

            // Act
            account = accountService.Withdraw(accountId, amount);

            // Assert
            Assert.IsNull(account);
            logServiceMock.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void BalanceShouldReturnAccount()
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var operationJournalMock = new Mock<IOperationJournalService>();
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account
            {
                Amount = 100,
                User = new User
                {
                    AtmCard = new AtmCard
                    {
                        Id = 1
                    }
                }
            });
            operationJournalMock.Setup(m =>
                m.LogOperation(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OperationCode>()));

            var accountId = 1;
            var accountService = new AccountService(null, accountRepositoryMock.Object, operationJournalMock.Object, null);
            Account account;

            // Act
            account = accountService.Balance(accountId);

            // Assert
            Assert.IsNotNull(account);
            operationJournalMock.Verify(x => x.LogOperation(It.IsAny<int>(), It.IsAny<int>(), OperationCode.Balance), Times.Once);
        }
    }
}
