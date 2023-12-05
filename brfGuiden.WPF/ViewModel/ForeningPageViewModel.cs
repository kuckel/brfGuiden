using CommunityToolkit.Mvvm.ComponentModel;
using System;

using System.ComponentModel;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using brfGuiden.Core.Interface;
using brfGuiden.WPF.ViewModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using brfGuiden.WPF.View;
using brfGuiden.WPF.Helper;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;

namespace brfGuiden.WPF.ViewModel
{
    public partial class ForeningPageViewModel : ObservableObject, INotifyPropertyChanged
    {

     
        private readonly IForeningService? _foreningService;
        private readonly Util? _util;
        private Forening? _forening;

        public ForeningPageViewModel(IForeningService foreningservice)
        {
            _foreningService = foreningservice;
            
            _util= new Util();
            _forening = _foreningService.GetForening();
            
            if(_forening==null)//First time use
            {
                Forening newForening = new Forening();
                newForening.ForeningId = "00000000-0000-0000-0000-000000000000";
                _foreningService.AddForening(newForening); 
            }

        }

        [RelayCommand]
        public void Reload()
        {
            if (_forening != null)
            {
              _forening = _foreningService.GetForening();               
            }

        }


      
        
        public Forening Forening
        {
            get { return _forening; }
            set
            {
                _forening = value;
                OnPropertyChanged(nameof(Forening));
                MessageBox.Show("PropChanged");
            }
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


        private async void TimeLoop(int ms)
        {
            await Task.Delay(ms);
        }


        [RelayCommand]
        public void DoUpdate()
        {
           
            
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Forening, serviceProvider: null, items: null);

            if (Validator.TryValidateObject(Forening, validationContext, validationResults, validateAllProperties: true) && _forening!=null)
            {
                
                Forening updForening = _foreningService.UpdateForening(_forening);
                if (updForening != null)
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


                }
                else
                {
                    MessageBox.Show("Ett fel uppstod när föreningen skulle uppdateras", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }                    



            }
            else
            {
                
                foreach (var validationResult in validationResults)
                {
                    if(validationResult.MemberNames.Contains("Namn")) { TxtNameErrorMessage = validationResult.ErrorMessage.ToString(); }
                    if (validationResult.MemberNames.Contains("OrgNr")) { TxtOrgNrErrorMessage= validationResult.ErrorMessage.ToString(); }
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




        #region VALIDATION



        #endregion 
    }
}
