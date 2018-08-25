using Atm.Core;
using Atm.Data;
using CrossCutting.Core.Logging;

namespace Atm.Application
{
    public class AtmCardService : IAtmCardService
    {
        private readonly IAtmCardRepository _atmCardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private readonly IOperationJournalService _operationJournalService;

        public AtmCardService(IUnitOfWork unitOfWork, IAtmCardRepository atmCardRepository, IOperationJournalService operationJournalService, ILogService logService)
        {
            _atmCardRepository = atmCardRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
            _operationJournalService = operationJournalService;
        }

        public AtmCard GetCardByNumber(string cardNumber)
        {
            var atmCard = _atmCardRepository.GetByAtmCardNumber(cardNumber);
            return atmCard;
        }

        public User ValidateAtmCardPin(string cardNumber, string pin)
        {
            var atmCard = _atmCardRepository.GetByAtmCardNumber(cardNumber);
            if (atmCard.Pin == pin)
            {
                return atmCard.User;
            }

            return null;
        }
    }
}