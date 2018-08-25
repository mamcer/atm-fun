using Atm.Core;

namespace Atm.Application
{
    public interface IAccountService
    {
        bool HasEnoughFunds(int accountId, decimal amount);

        Account Withdraw(int accountId, decimal amount);

        Account Balance(int accountId);
    }
}