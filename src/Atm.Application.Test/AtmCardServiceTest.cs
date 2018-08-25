using Atm.Core;
using Atm.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Application.Test
{
    [TestClass]
    public class AtmCardServiceTest
    {
        [TestMethod]
        public void GetCardByNumberShouldReturnCard()
        {
            // Arrange
            var atmCardRepositoryMock = new Mock<IAtmCardRepository>();
            atmCardRepositoryMock.Setup(m => m.GetByAtmCardNumber(It.IsAny<string>())).Returns(new AtmCard
            {
                Id = 99
            });
            var atmCardService = new AtmCardService(null, atmCardRepositoryMock.Object, null, null);
            AtmCard result;
            string cardNumber = "1234";

            // Act
            result = atmCardService.GetCardByNumber(cardNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(99, result.Id);
        }

        [TestMethod]
        public void ValidateAtmCardPinWithValidPinShouldReturnUser()
        {
            // Arrange
            var atmCardRepositoryMock = new Mock<IAtmCardRepository>();
            atmCardRepositoryMock.Setup(m => m.GetByAtmCardNumber(It.IsAny<string>())).Returns(new AtmCard
            {
                Id = 99,
                Pin = "666",
                User = new User
                {
                    Id = 77
                }
            });
            var atmCardService = new AtmCardService(null, atmCardRepositoryMock.Object, null, null);
            User result;
            string cardNumber = "1234";
            string pin = "666";

            // Act
            result = atmCardService.ValidateAtmCardPin(cardNumber, pin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(77, result.Id);
        }

        [TestMethod]
        public void ValidateAtmCardPinWithInvalidPinShouldReturnNull()
        {
            // Arrange
            var atmCardRepositoryMock = new Mock<IAtmCardRepository>();
            atmCardRepositoryMock.Setup(m => m.GetByAtmCardNumber(It.IsAny<string>())).Returns(new AtmCard
            {
                Id = 99,
                Pin = "777"
            });
            var atmCardService = new AtmCardService(null, atmCardRepositoryMock.Object, null, null);
            User result;
            string cardNumber = "1234";
            string pin = "666";

            // Act
            result = atmCardService.ValidateAtmCardPin(cardNumber, pin);

            // Assert
            Assert.IsNull(result);
        }
    }
}