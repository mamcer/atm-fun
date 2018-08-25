using Atm.Core;

namespace Atm.Application
{
    public interface IAtmCardService
    {
        AtmCard GetCardByNumber(string cardNumber);

        User ValidateAtmCardPin(string cardNumber, string pin);
    }
}