using Atm.Core;

namespace Atm.Application
{
    public interface IOperationJournalService
    {
        void LogOperation(int cardId, int accountId, OperationCode code);
    }
}