using Keypass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByEmail(string email);
        Account GetByUsername(string username);
        Account Update(int id, Account entity);        
    }
}
