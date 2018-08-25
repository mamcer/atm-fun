using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atm.Core;
using Atm.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Application.Test
{
    [TestClass]
    public class OperationJournalServiceTest
    {
        [TestMethod]
        public void LogOperationShouldWriteEntry()
        {
            // Arrange
            var atmCardRepositoryMock = new Mock<IAtmCardRepository>();
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var operationJournalRepositoryMock = new Mock<IOperationJournalRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            atmCardRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new AtmCard()
            {
                Id = 88
            });
            accountRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Account()
            {
                Amount = 100
            });
            operationJournalRepositoryMock.Setup(m => m.Create(It.IsAny<OperationJournal>()));
            unitOfWorkMock.Setup(m => m.SaveChanges());
            var cardId = 1;
            var accountId = 2;
            var operationCode = OperationCode.Balance;
            var operationJournalService = new OperationJournalService(unitOfWorkMock.Object, operationJournalRepositoryMock.Object, atmCardRepositoryMock.Object, accountRepositoryMock.Object, null);

            // Act
            operationJournalService.LogOperation(cardId, accountId, operationCode);

            // Assert
            atmCardRepositoryMock.Verify(m => m.GetById(cardId), Times.Once);
            accountRepositoryMock.Verify(m => m.GetById(accountId), Times.Once);
            operationJournalRepositoryMock.Verify(m => m.Create(It.IsAny<OperationJournal>()), Times.Once);
            unitOfWorkMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}