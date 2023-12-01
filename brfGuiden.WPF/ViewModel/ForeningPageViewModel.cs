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


        //private string? _organizationNumber;
        //[Required(ErrorMessage = "Fältet är obligatoriskt.")]
        //[MinLength(12, ErrorMessage = "OrgNr måste bestå av 12 tecken")]
        //[MaxLength(12, ErrorMessage = "OrgNr måste bestå av max 12 tecken")]
        //public string OrganizationNumber
        //{
        //    get { return _organizationNumber ?? ""; }
        //    set
        //    {
        //        if (_organizationNumber != value)
        //        {
        //            _organizationNumber = value;
        //            OnPropertyChanged(nameof(OrganizationNumber));
        //        }
        //    }
        //}


        //private string? _postNr;
        //[Required(ErrorMessage = "Fältet är obligatoriskt.")]
        //[RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "Felaktigt format i PostNr.")]
        //[MaxLength(6, ErrorMessage = "Max antal tecken är 6")]
        //[MinLength(5, ErrorMessage = "Fel antal tecken i PostNr")]
        //public string PostNr
        //{
        //    get { return _postNr ?? ""; }
        //    set
        //    {
        //        if (_postNr != value)
        //        {
        //            _postNr = value;
        //            OnPropertyChanged(nameof(PostNr));
        //        }
        //    }
        //}

        //private string? _foreningNamn;
        //[Required(ErrorMessage = "Fältet är obligatoriskt.")]
        //[MaxLength(40, ErrorMessage = "Max antal tecken är 40")]     

        //public string ForeningNamn
        //{
        //    get { return _foreningNamn ?? ""; }
        //    set
        //    {
        //        if (_foreningNamn != value)
        //        {
        //            _foreningNamn = value;
        //            OnPropertyChanged(nameof(ForeningNamn));
        //        }
        //    }
        //}

        //private string? _foreningAdress;
        //[MaxLength(40, ErrorMessage = "Max antal tecken är 40")]
        //public string ForeningAdress
        //{
        //    get { return _foreningAdress ?? ""; }
        //    set
        //    {
        //        if (_foreningAdress != value)
        //        {
        //            _foreningAdress = value;
        //            OnPropertyChanged(nameof(ForeningAdress));
        //        }
        //    }
        //}

        //private string? _postOrt;
        //[MaxLength(40, ErrorMessage = "Max antal tecken är 40")]
        //public string PostOrt
        //{
        //    get { return _foreningAdress ?? ""; }
        //    set
        //    {
        //        if (_postOrt != value)
        //        {
        //            _postOrt = value;
        //            OnPropertyChanged(nameof(PostOrt));
        //        }
        //    }
        //}




        #region VALIDATION

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        var validationContext = new ValidationContext(this, null, null) { MemberName = columnName };
        //        List<ValidationResult> validationResults = new List<ValidationResult>();
        //        if (Validator.TryValidateProperty(GetType().GetProperty(columnName).GetValue(this), validationContext, validationResults))
        //        {
        //            if (_errors.ContainsKey(columnName))
        //                _errors.Remove(columnName);
        //            if (_errors != null && _errors.Count > 0) { HasErrors = true; } else { HasErrors = false; }
        //            return null;
        //        }

        //          _errors[columnName] = validationResults.Select(r => r.ErrorMessage).ToList();
        //        if (_errors != null && _errors.Count > 0) { HasErrors = true; } else { HasErrors = false; }
        //        return validationResults.First().ErrorMessage;


        //    }
        //}

        #endregion 
    }
}
