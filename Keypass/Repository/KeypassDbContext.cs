using Keypass.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class KeypassDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Service> Services { get; set; }
        public KeypassDbContext(DbContextOptions options) : base(options) { }
    }
}
