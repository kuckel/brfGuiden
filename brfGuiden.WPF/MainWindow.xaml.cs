using brfGuiden.Core.Interface;
using brfGuiden.Core.Service;
using brfGuiden.WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Contracts;

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
