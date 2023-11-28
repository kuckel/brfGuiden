#nullable disable 
using brfGuiden.WPF.ViewModel;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace brfGuiden.WPF.ViewModel
{
    public class ViewModelLocator
    {
        // Singleton instance of ViewModelLocator
        private static ViewModelLocator _instance;
        private  static IKernel _kernel;
      
       
        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator(_kernel));

        public MainWindowViewModel MainViewModel =>  _kernel.Get<MainWindowViewModel>();
        
        public LoginWindowViewModel LoginWindowViewModel => _kernel.Get<LoginWindowViewModel>();

        public DashboardViewModel DashboardViewModel => _kernel.Get<DashboardViewModel>();

        public ForeningPageViewModel ForeningPageViewModel => _kernel.Get<ForeningPageViewModel>();

        public InstallningPageViewModel InstallningPageViewModel => _kernel.Get<InstallningPageViewModel>();

        public BostadPageViewModel BostadPageViewModel => _kernel.Get<BostadPageViewModel>();

        public StyrelsePageViewModel StyrelsePageViewModel => _kernel.Get<StyrelsePageViewModel>();

        public ViewModelLocator(IKernel kernel)
        {
            _kernel = kernel;
        }

    }        

}







