using Keypass.Repository;
using Keypass.Repository.Interfaces;
using Keypass.Services.AuthenticationService;
using Keypass.Services.FileService;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host) 
        {
            host.ConfigureServices(services => 
            {                
                services.AddSingleton<IPasswordHasher, PasswordHasher>();                
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IAccountRepository, AccountRepository>();
                services.AddSingleton<IServiceRepository, ServiceRepository>();
                services.AddSingleton<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<IFileImport, FileImport>();
                services.AddSingleton<IFileExport, FileExport>();
            });

            return host;
        }
    }
}
