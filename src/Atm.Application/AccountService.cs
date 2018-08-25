using System;
using Atm.Core;
using Atm.Data;
using CrossCutting.Core.Logging;

namespace Atm.Application
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private readonly IOperationJournalService _operationJournalService;

        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IOperationJournalService operationJournalService, ILogService logService)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
            _operationJournalService = operationJournalService;
        }

        private static readonly object SyncToken = new object();

        public bool HasEnoughFunds(int accountId, decimal amount)
        {
            var account = _accountRepository.GetById(accountId);
            return account.Amount - amount >= 0;
        }

        public Account Withdraw(int accountId, decimal amount)
        {
            try
            {
                lock (SyncToken)
                {
                    var account = _accountRepository.GetById(accountId);
                    _operationJournalService.LogOperation(account.User.AtmCard.Id, accountId, OperationCode.Withdrawal);

                    account.Amount -= amount;

                    _accountRepository.Update(account);
                    _unitOfWork.SaveChanges();

                    return account;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message);
                return null;
            }
        }

        public Account Balance(int accountId)
        {
            var account = _accountRepository.GetById(accountId);
            _operationJournalService.LogOperation(account.User.AtmCard.Id, accountId, OperationCode.Balance);

            return account;
        }
    }
}