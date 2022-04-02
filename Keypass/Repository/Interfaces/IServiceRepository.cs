using Keypass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        IEnumerable<Service> SearchByText(string text);
        IEnumerable<Service> SortByDate();
        IEnumerable<Service> SortByTitle();
        IEnumerable<Service> SortByUsername();
        IEnumerable<Service> GetWithInclude();
    }
}
