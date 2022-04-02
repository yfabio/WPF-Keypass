using Keypass.Domain;
using Keypass.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context) { }
       
        public Account GetByEmail(string email)
        {
            return KeypassDbContext.Accounts.Where(e => e.Email.Equals(email)).FirstOrDefault();
        }

        public Account GetByUsername(string username)
        {
            return KeypassDbContext.Accounts.Where(e => e.Username.Equals(username)).FirstOrDefault();
        }

        public Account Update(int id, Account entity)
        {
            entity.Id = id;
            KeypassDbContext.Update(entity);
            return entity;
        }


        



    }
}
