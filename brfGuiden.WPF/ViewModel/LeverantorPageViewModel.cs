using brfGuiden.Models;
using brfGuiden.WPF.Helper;
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
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.ViewModel
{
    public partial class LeverantorPageViewModel: ObservableObject
    {
        private ObservableCollection<Leverantor>? _leverantorer { get; set; }
        private readonly ILeverantorService? _leverantorService;
        private readonly INavigationService? _navigationService;
        private Leverantor? _selectedLeverantor;
        private readonly Util _util;        



        public LeverantorPageViewModel(ILeverantorService leverantorservice, INavigationService navigationService)
        {
            _leverantorService=leverantorservice;
            _navigationService=navigationService;
               _util = new Util(); 
              _leverantorer = _leverantorService.GetLeverantorerCollection();
            _countLeverantorer = _leverantorer.Count();
        }



        public Leverantor SelectedLeverantor
        {
            get
            {
                return _selectedLeverantor;
            }
            set
            {
                _selectedLeverantor = value;
                OnPropertyChanged(nameof(SelectedLeverantor));
            }
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
            



        [RelayCommand]
        public void EscKeyPress()
        {

            Leverantorer.Clear();
            LoadProgressSpinner(1000);
            Leverantorer = _leverantorService.GetLeverantorerCollection();  

        }

       

        [RelayCommand]
        public void Change(Leverantor lev)
        {
            if(lev != null)
            {

                

                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                mw.DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();
                var activeItem = (NavigationViewItem?)mw.navMain.MenuItems[4];
                if (activeItem != null)
                {

                    EditLeverantorPageViewModel x = new EditLeverantorPageViewModel(lev);


                    mw.navMain.Navigate(typeof(EditLeverantorPage), x );
                    
                    activeItem.IsActive = true; //Markera i menyn                    

                }
            }
            else
            {
                ShowError("Vänligen välj en leverantör i listan","Saknar leverantör");
                return; 
            }

        }



        [RelayCommand]
        public void Delete(Leverantor lev)
        {

            if(lev==null)
            {
                ShowError("Vänligen välj en leverantör i listan","Saknar leverantör");
                return;
            }

            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Är du säker på att du vill radera den valda leverantören?", "Radera", System.Windows.MessageBoxButton.YesNo);
            switch (result)
            {
                case System.Windows.MessageBoxResult.No:
                    return;
            }


            bool delResult = _leverantorService.DeleteLeverantor(lev);

            // Show loading
            LoadingWindow lw = new LoadingWindow();
            lw.TimeSleep = 3000;
            lw.Owner = Application.Current.MainWindow;
            foreach (var window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    lw.Width = ((Window)window).Width;
                    lw.Height = ((Window)window).Height;
                }
            }

            lw.ShowDialog();

            if (delResult)
            {
                 
                _leverantorer = _leverantorService.GetLeverantorerCollection();   

            }
            else
            {
                ShowError("Ett fel uppstod när leverantören skulle raderas","Error");
                return;
            }

        }


        private void ShowError(string errorMessage, string title)
        {
            var messageBox = new Wpf.Ui.Controls.MessageBox();
            var converter = new System.Windows.Media.BrushConverter();
            messageBox.BorderBrush = _util.ConvertColour("#000000");
            messageBox.Background = _util.ConvertColour("#404040");
            messageBox.BorderThickness = new System.Windows.Thickness(2);
            messageBox.Content = errorMessage;
            messageBox.Title = title;
            messageBox.CloseButtonText = "Stäng";
            messageBox.ShowDialogAsync();
        }
            


        private void LoadProgressSpinner(int ms)
        {

            LoadingWindow lw = new LoadingWindow();
            lw.TimeSleep = ms;
            lw.Owner = Application.Current.MainWindow;
            foreach (var window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    lw.Width = ((Window)window).Width;
                    lw.Height = ((Window)window).Height;

                }
            }
            lw.ShowDialog();


        }

        private int _countLeverantorer;
        public int CountLeverantorer
        {
            get { return _countLeverantorer; }
            set
            {
                _countLeverantorer = value;
                OnPropertyChanged(nameof(CountLeverantorer));
                
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
