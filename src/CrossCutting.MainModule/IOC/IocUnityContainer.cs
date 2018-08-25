using System;
using System.Configuration;
using System.Data.Entity;
using CrossCutting.Core.IOC;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.Logging;
using Microsoft.Practices.Unity;
using Atm.Application;
using Atm.Data;

namespace CrossCutting.MainModule.IOC
{
    public class IocUnityContainer : IContainer
    {
        private UnityContainer UnityContainer { get; }

        public IocUnityContainer() : this(new UnityContainer())
        {}

        public IocUnityContainer(UnityContainer container)
        {
            UnityContainer = container;
            RegisterTypes();
        }

        public T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return UnityContainer.Resolve(type);
        }

        public void RegisterTypes()
        {
            bool realContainer = true;
            if (ConfigurationManager.AppSettings["IocRealContainer"] != null &&
                bool.TryParse(ConfigurationManager.AppSettings["IocRealContainer"], out realContainer) == false)
            {
                realContainer = true;
            }

            if (realContainer)
            {
                RegisterRealTypes();
            }
        }

        private void RegisterRealTypes()
        {
            UnityContainer.RegisterType<ILogService, FileLogService>();

            UnityContainer.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            UnityContainer.RegisterType<DbContext, AtmEntities>(new HierarchicalLifetimeManager());

            UnityContainer.RegisterType<IAccountRepository, AccountRepository>();
            UnityContainer.RegisterType<IUserRepository, UserRepository>();
            UnityContainer.RegisterType<IAtmCardRepository, AtmCardRepository>();
            UnityContainer.RegisterType<IOperationJournalRepository, OperationJournalRepository>();

            UnityContainer.RegisterType<IAtmCardService, AtmCardService>();
            UnityContainer.RegisterType<IUserService, UserService>();
            UnityContainer.RegisterType<IAccountService, AccountService>();
            UnityContainer.RegisterType<IOperationJournalService, OperationJournalService>();
        }
    }
}