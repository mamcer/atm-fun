using Atm.Core;

namespace Atm.Application
{
    public interface IUserService
    {
        User GetById(int id);
    }
}