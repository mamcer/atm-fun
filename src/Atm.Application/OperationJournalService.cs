using System;
using Atm.Core;
using Atm.Data;
using CrossCutting.Core.Logging;

namespace Atm.Application
{
    public class OperationJournalService : IOperationJournalService
    {
        private readonly IOperationJournalRepository _operationJournalRepository;
        private readonly IAtmCardRepository _atmCardRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;

        public OperationJournalService(IUnitOfWork unitOfWork, IOperationJournalRepository operationJournalRepository, IAtmCardRepository atmCardRepository, IAccountRepository accountRepository, ILogService logService)
        {
            _operationJournalRepository = operationJournalRepository;
            _atmCardRepository = atmCardRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
        }

        public void LogOperation(int cardId, int accountId, OperationCode code)
        {
            var card = _atmCardRepository.GetById(cardId);
            var account = _accountRepository.GetById(accountId);
            var operationJournal = new OperationJournal
            {
                AtmCard = card,
                Amount = account.Amount,
                Date = DateTime.Now,
                OperationCode = code
            };

            _operationJournalRepository.Create(operationJournal);
            _unitOfWork.SaveChanges();
        }
    }
}