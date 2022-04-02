using Keypass.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Repository
{
    public class ContentProvider
    {
        public ObservableCollection<Service> services = new ObservableCollection<Service>();

        public ObservableCollection<Service> Services
        {
            get { return services; }            
        }

        public ContentProvider()
        {
            Load();
        }

        private void Load()
        {

            Account user = new Account { Id = 1, Username = "user-1", Password = "pass" };

            services.Add(new Service { Id = 1, Title = "title-1", Created = DateTime.Now, Username = "user-1", Password = "pass-1", Comment = "comment-1", Account = user });

            services.Add(new Service { Id = 2, Title = "title-2", Created = DateTime.Now, Username = "user-2", Password = "pass-2", Comment = "comment-2", Account = user });

            services.Add(new Service { Id = 3, Title = "title-3", Created = DateTime.Now, Username = "user-3", Password = "pass-3", Comment = "comment-3", Account = user });

            services.Add(new Service { Id = 4, Title = "title-4", Created = DateTime.Now, Username = "user-4", Password = "pass-4", Comment = "comment-4", Account = user });

            services.Add(new Service { Id = 5, Title = "title-5", Created = DateTime.Now, Username = "user-5", Password = "pass-5", Comment = "comment-5", Account = user });



        }

    }
}
