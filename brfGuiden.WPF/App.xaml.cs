#nullable disable 
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


using brfGuiden.WPF.ViewModel;
using brfGuiden.Core.Interface;
using brfGuiden.Core.Service;
using brfGuiden.WPF.View;
using brfGuiden.WPF.Helper;
using System.IO;

using brfGuiden.WPF.Interface;
using brfGuiden.WPF.Service;
using brfGuiden.Models;
using Wpf.Ui;

namespace brfGuiden.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private Util _util;
        private ConfigHelper _configHelper;
        public static IServiceProvider ServiceProvider { get; set; }


        private void App_Startup(object sender, StartupEventArgs e)
        {

            _util = new Util();
            _configHelper = new ConfigHelper();
            var services = new ServiceCollection();

            // Services 
            services.AddScoped<IForeningService, ForeningService>();
            services.AddScoped<IStyrelseService, StyrelseService>();
            services.AddScoped<IServiceProvider, ServiceProvider>();
            services.AddScoped<ISnackbarService, SnackbarService >();
            services.AddScoped<ILeverantorService, LeverantorService>();
            services.AddScoped<IContentDialogService, ContentDialogService>();
            services.AddScoped<INavigationService, NavigationService>();
            
            services.AddSingleton<IContentDialogService, ContentDialogService>();

            // View-models
            services.AddTransient<BostadPageViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<ForeningPageViewModel>();
            services.AddTransient<InstallningPageViewModel >();
            services.AddTransient<MedlemPageViewModel>();
            services.AddTransient<LeverantorPageViewModel>();
            services.AddTransient<StyrelsePageViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AddLeverantorPageViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<DashboardPage>();
            services.AddTransient<dataContext>();
            
            ServiceProvider = services.BuildServiceProvider();


            // Read username, computer etc 
            WriteToAppConfig();

            MainWindow mw = new MainWindow();
            mw.Show();
            mw.navMain.Navigate(typeof(DashboardPage)); // Set default-page 
             
        }           



        public static T GetService<T>() where T : class
        {
            if (ServiceProvider != null)
            {
             return (T)ServiceProvider?.GetService(typeof(T));              
            }
            else
            {
                throw new ArgumentNullException(nameof(ServiceProvider));
            }
        }




        private void WriteToAppConfig()
        {
            if (string.IsNullOrEmpty(_configHelper?.ReadFromAppConfig("User")) && _util != null)
            {
                string UserName = _util.GetComputerLoggedOnUser() ?? "";
                _configHelper?.WriteToAppConfig("User", UserName);
            }
            if (string.IsNullOrEmpty(_configHelper?.ReadFromAppConfig("Computer")) && _util != null)
            {
                string Computer = _util.GetComputerName() ?? "";
                _configHelper?.WriteToAppConfig("Computer", Computer);
            }

            //-- Write to app.config loggedIN-Date
            _configHelper?.WriteToAppConfig("LoggedIN", DateTime.Now.ToString());
        }
                
         





    }



}

