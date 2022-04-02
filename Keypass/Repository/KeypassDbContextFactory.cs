using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class KeypassDbContextFactory
    {

        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public KeypassDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public KeypassDbContext CreateDbContext() 
        {
            DbContextOptionsBuilder<KeypassDbContext> options = new DbContextOptionsBuilder<KeypassDbContext>();
            _configureDbContext(options);
            return new KeypassDbContext(options.Options);
        }
    }
}
