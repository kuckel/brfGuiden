

using brfGuiden.WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace brfGuiden.WPF.View
{
    /// <summary>
    /// Interaction logic for Forening.xaml
    /// </summary>
    public partial class ForeningPage 
    {
        public ForeningPage() 
        {
            DataContext = App.ServiceProvider.GetService<ForeningPageViewModel>();
            InitializeComponent();
 
        }            

    }
}
