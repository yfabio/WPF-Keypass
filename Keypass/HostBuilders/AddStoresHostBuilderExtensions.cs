using Keypass.Repository;
using Keypass.Repository.Interfaces;
using Keypass.Services.AuthenticationService;
using Keypass.Services.Criptography;
using Keypass.State.Accounts;
using Keypass.State.Authenticators;
using Keypass.State.Navigators;
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
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host) 
        {
            host.ConfigureServices(services => {
                services.AddSingleton<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<IPasswordCryptor, PasswordCryptor>();
            });
            return host;
        }
    }
}
