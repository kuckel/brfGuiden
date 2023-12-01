using brfGuiden.Core.Interface;
using brfGuiden.Core.Service;
using brfGuiden.WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;

using Wpf.Ui.Controls;

namespace brfGuiden.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow   
    {
      

        public MainWindow()
        {
           

            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();

        }



            

    }
}
