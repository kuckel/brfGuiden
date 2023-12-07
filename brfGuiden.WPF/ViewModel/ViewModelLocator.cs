using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.ViewModel
{
    public class ViewModelLocator
    {

        // Singleton instance of ViewModelLocator
        private static ViewModelLocator? _instance;
        private readonly IServiceProvider _serviceProvider;

        public static ViewModelLocator Instance => _instance ??= new ViewModelLocator();

        // Add additional view models as needed
        public MainWindowViewModel MainWindowViewModel => App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
        public AddLeverantorPageViewModel AddLeverantorPageViewModel => App.ServiceProvider.GetRequiredService<AddLeverantorPageViewModel>();
        public DashboardViewModel DashboardViewModel => App.ServiceProvider.GetRequiredService<DashboardViewModel>();
        public ForeningPageViewModel ForeningPageViewModel => App.ServiceProvider.GetRequiredService<ForeningPageViewModel>();


        // Private constructor to enforce singleton pattern
        private ViewModelLocator()
        {
            // Setup and configure your dependency injection container
            var services = new ServiceCollection();

            // Register your view models
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<AddLeverantorPageViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<ForeningPageViewModel>();



            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();
        }

    }
}
