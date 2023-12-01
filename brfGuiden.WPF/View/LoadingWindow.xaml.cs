using brfGuiden.WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace brfGuiden.WPF.View
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingWindow 
    {
        public LoadingWindow()
        {
            InitializeComponent();

        }            



        /// <summary>
        /// Thread sleep in milliseconds
        /// </summary>
        public int TimeSleep { get; set; } = 1000;


        private async void WaitForSeconds(int ms)
        {


            await Task.Delay(ms);
            this.Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

            WaitForSeconds(TimeSleep);
        }


    }
}
