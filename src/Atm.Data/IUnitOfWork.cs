using System;

namespace Atm.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}