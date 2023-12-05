using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using brfGuiden.WPF.View;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.ViewModel
{
    public partial class LeverantorPageViewModel: ObservableObject
    {
        private ObservableCollection<Leverantor>? _leverantorer { get; set; }
        private readonly ILeverantorService? _leverantorService;
        private readonly MainWindow _mainWin;

        public LeverantorPageViewModel(ILeverantorService leverantorservice, MainWindow mainwindow)
        {
            _leverantorService=leverantorservice;
            _mainWin = mainwindow;
              _leverantorer = _leverantorService.GetLeverantorerCollection();  

        }           


        public ObservableCollection<Leverantor> Leverantorer
        {
            get { return _leverantorer; }
            set
            {
                if (_leverantorer != value)
                {
                    _leverantorer = value;
                    OnPropertyChanged(nameof(Leverantorer));
                }
            }
        }

        [RelayCommand]
        public void NavigateToAddLeverantor()
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();
            var activeItem = (NavigationViewItem?)mw.navMain.MenuItems[4];
            if(activeItem!=null)
            {
                mw.navMain.Navigate(typeof(AddLeverantorPage));
                activeItem.IsActive = true; //Markera i menyn
            }             
        }                   



        private string? _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);

    }
}
