using System.Collections.Generic;
using System.Data.Entity;
using Atm.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Atm.Data.Test
{
    [TestClass]
    public class AtmCardRepositoryTest
    {
        [TestMethod]
        public void GetByAtmCardNumberShouldReturnAtmCard()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var cards = new List<AtmCard>
            {
                new AtmCard
                {
                    Id = 1,
                    Number = "123"
                },
                new AtmCard
                {
                    Id = 107,
                    Number = "1234"
                },
                new AtmCard
                {
                    Id = 3,
                    Number = "1235"
                },
            };
            context.Setup(m => m.Set<AtmCard>()).Returns(new FakeSet<AtmCard>(cards));
            var atmCardRepository = new AtmCardRepository(context.Object);
            AtmCard card;
            var cardNumber = "1234";
            
            // Act
            card = atmCardRepository.GetByAtmCardNumber(cardNumber);

            // Assert
            Assert.IsNotNull(card);
            Assert.AreEqual(107, card.Id);
        }
    }
}
