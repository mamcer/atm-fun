using Atm.Core;
using Atm.Data;
using CrossCutting.Core.Logging;

namespace Atm.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private readonly IOperationJournalService _operationJournalService;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOperationJournalService operationJournalService, ILogService logService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
            _operationJournalService = operationJournalService;
        }

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }
    }
}