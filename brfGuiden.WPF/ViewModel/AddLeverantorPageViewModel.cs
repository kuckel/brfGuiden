using brfGuiden.Models;
using brfGuiden.WPF.Helper;
using brfGuiden.WPF.Interface;
using brfGuiden.WPF.Service;
using brfGuiden.WPF.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.ViewModel
{
    public partial class AddLeverantorPageViewModel:ObservableObject
    {

        private ObservableCollection<Leverantor>? _leverantorer { get; set; }
        private readonly ILeverantorService? _leverantorService;
        private Leverantor? _leverantor;
        private readonly Util? _util;

        public AddLeverantorPageViewModel(ILeverantorService leverantorservice)
        {
            _leverantorService= leverantorservice;
            _util= new Util();
            _leverantor = new Leverantor{LeverantorId = Guid.NewGuid().ToString()}; 
        }


        public Leverantor Leverantor
        {
            get { return _leverantor; }
            set
            {
                _leverantor = value;
                OnPropertyChanged(nameof(Leverantor));
            }
        }


        [RelayCommand]
        public void Save()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Leverantor, serviceProvider: null, items: null);

            if (Validator.TryValidateObject(Leverantor, validationContext, validationResults, validateAllProperties: true) && _leverantor != null)
            {

                Leverantor newLev = _leverantorService.AddLeverantor(_leverantor);
                if (newLev != null)
                {

                    // Show loading
                    LoadingWindow lw = new LoadingWindow();
                    lw.TimeSleep = 4000;
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

                    MainWindow mw = (MainWindow)Application.Current.MainWindow;
                    mw.DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();
                    var activeItem = (NavigationViewItem?)mw.navMain.MenuItems[4];
                    if (activeItem != null)
                    {
                        mw.navMain.Navigate(typeof(LeverantorPage));
                        activeItem.IsActive = true; //Markera i menyn
                    }



                }
                else
                {
                    ShowError("Ett fel uppstod när leverantören skulle skapas");
                    return;
                }                    




            }
            else
            {

                foreach (var validationResult in validationResults)
                {
                    ShowError(validationResult.ErrorMessage.ToString());
                    HasErrors = true;
                }                    

            }


        }


        private void ShowError(string errorMessage)
        {

            var messageBox = new Wpf.Ui.Controls.MessageBox();
            messageBox.BorderThickness = new System.Windows.Thickness(2);
            var converter = new System.Windows.Media.BrushConverter();
            messageBox.BorderBrush = _util.ConvertColour("#000000");
            messageBox.Background = _util.ConvertColour("#404040");
            messageBox.BorderThickness = new System.Windows.Thickness(2);
            messageBox.Content = errorMessage;
            messageBox.Title = "Valideringsfel";
            messageBox.CloseButtonText = "Stäng";
            messageBox.ShowDialogAsync();
        }



        private string? _txtNameErrorMessage;
        public string TxtNameErrorMessage
        {
            get { return _txtNameErrorMessage; }
            set
            {
                _txtNameErrorMessage = value;
                OnPropertyChanged(nameof(TxtNameErrorMessage));
            }
        }
        private string? _txtOrgNrErrorMessage;
        public string TxtOrgNrErrorMessage
        {
            get { return _txtOrgNrErrorMessage; }
            set
            {
                _txtOrgNrErrorMessage = value;
                OnPropertyChanged(nameof(TxtOrgNrErrorMessage));
            }
        }

        private bool _isNameFocused;
        public bool IsNameFocused
        {
            get { return _isNameFocused; }
            set
            {
                _isNameFocused = value;
                OnPropertyChanged(nameof(IsNameFocused));
            }
        }
        private bool _isOrgNrFocused;
        public bool IsOrgNrFocused
        {
            get { return _isOrgNrFocused; }
            set
            {
                _isOrgNrFocused = value;
                OnPropertyChanged(nameof(IsOrgNrFocused));
            }
        }

        private bool _hasErrors;
        public bool HasErrors
        {
            get { return _hasErrors; }
            set
            {
                if (_hasErrors != value)
                {
                    _hasErrors = value;
                    OnPropertyChanged(nameof(HasErrors));
                }
            }
        }



    }
}
