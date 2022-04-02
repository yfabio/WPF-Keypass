using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Keypass.State.Authenticators;
using Keypass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keypass.State.Navigators;
using Keypass.ViewModels.Factories;
using Keypass.State.Accounts;
using Keypass.Repository.Interfaces;
using Keypass.Services.Criptography;
using Keypass.Services.FileService;
using Microsoft.AspNet.Identity;

namespace Keypass.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddTransient<AddViewModel>();
                services.AddTransient<OptionViewModel>();
               
                services.AddSingleton<IKeyPassViewModelFactory, KeyPassViewModelFactory>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => CreateHomeViewModel(services)); 
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));          
                services.AddSingleton<CreateViewModel<AddViewModel>>(services => () => CreateAddViewModel(services));   
                services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();                
                services.AddSingleton<CreateViewModel<OptionViewModel>>(services => () => CreateOptionViewModel(services));


            });

            return host;
        }


        private static OptionViewModel CreateOptionViewModel(IServiceProvider services)
        {
            return new OptionViewModel(
                services.GetRequiredService<IFileImport>(),
                services.GetRequiredService<IFileExport>(),
                services.GetRequiredService<IUnitOfWork>(),
                services.GetRequiredService<IPasswordHasher>());
        }

        private static AddViewModel CreateAddViewModel(IServiceProvider services)
        {
            return new AddViewModel(services.GetRequiredService<IUnitOfWork>(),           
                services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                services.GetRequiredService<IPasswordCryptor>(),
                services.GetRequiredService<IAccountStore>());
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(services.GetRequiredService<IPasswordCryptor>(),                 
                services.GetRequiredService<IAccountStore>(),                                
                services.GetRequiredService<IUnitOfWork>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>()
                );
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(services.GetRequiredService<IAuthenticator>(),
                 services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                 services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>()
                );
        }
    }
}
