using Keypass.HostBuilders;
using Keypass.Repository;
using Keypass.Repository.Interfaces;
using Keypass.Services.AuthenticationService;
using Keypass.State.Accounts;
using Keypass.State.Authenticators;
using Keypass.State.Navigators;
using Keypass.ViewModels;
using Keypass.ViewModels.Factories;
using Keypass.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Windows;
using static Keypass.ViewModels.ViewModelBase;

namespace Keypass
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;


        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            KeypassDbContextFactory contextFactory = _host.Services.GetRequiredService<KeypassDbContextFactory>();
            //using (KeypassDbContext context = contextFactory.CreateDbContext()) 
            //{
            //    context.Database.Migrate();
            //}

            KeypassDbContext context = contextFactory.CreateDbContext();
           
            if (context!=null) {
                if (context.Database.EnsureCreated())
                {
                    Debug.WriteLine("Database has been created!");
                }
                else {
                    Debug.WriteLine("Database alaready exists!");
                }
            }


            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);            
        }
    }
}
