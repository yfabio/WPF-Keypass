using Keypass.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KeypassDbContext _context;

        public IAccountRepository Accounts { get; private set; }

        public IServiceRepository Services { get; private set; }

        public UnitOfWork(KeypassDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(context);
            Services = new ServiceRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
