using Keypass.Domain;
using Keypass.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(DbContext context) : base(context) { }

        public IEnumerable<Service> SearchByText(string text)
        {
            return KeypassDbContext.Services.Where(e => e.Title.ToUpper().Contains(text.ToUpper())).ToList();
        }

        public IEnumerable<Service> SortByDate()
        {
            return KeypassDbContext.Services.Include(e => e.Account).OrderByDescending(e => e.Created).ToList();          
        }

        public IEnumerable<Service> SortByTitle()
        {
            return KeypassDbContext.Services.OrderBy(e => e.Title).ToList();           
        }

        public IEnumerable<Service> SortByUsername()
        {
            return KeypassDbContext.Services.OrderBy(e => e.Username).ToList();            
        }

        public IEnumerable<Service> GetWithInclude() 
        {
            return KeypassDbContext.Services.Include(e => e.Account).ToList();
        }

    }
}
