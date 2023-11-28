using brfGuiden.Core.Interface;
using brfGuiden.Models;
using brfGuiden.WPF.Helper;
using brfGuiden.WPF.View;
using brfGuiden.WPF.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;
using Wpf.Ui.Services;

namespace brfGuiden.WPF.ViewModel
{
    public partial class LoginWindowViewModel: ObservableObject, INotifyPropertyChanged
    {

        private IStyrelseService _styrelseService;
        private ISnackbarService _snackbarService;
        private string? _applicationID;
        private Util _util;

        public LoginWindowViewModel(IStyrelseService styrelseservice  )
        {
            _styrelseService= styrelseservice;
            _util= new Util();
            _snackbarService = new SnackbarService(); 
            _applicationID= System.Configuration.ConfigurationManager.AppSettings.Get("ApplicationID")?.ToString();
        }

        private string _emailAddress = "test@test.se";
    
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public bool CanLogin => !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password);

       
        private string _password = "";

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string? _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage ?? "";
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }




        [RelayCommand]
        public void Login(Object obj)
        {
            PasswordBox? pwBox = obj as PasswordBox;
            if (pwBox != null)
            {
               Password = pwBox.Password;
            }

            var  isValidUser= _styrelseService.AuthenticateUser(EmailAddress, Password);
            if (isValidUser)
            {
                StyrelseMedlem? styrelseMedlem = _styrelseService.GetStyrelsen().Where(x => x.Epost == EmailAddress).FirstOrDefault();
                if(styrelseMedlem!=null)
                {
                    string json = _util.SerializeToJson<StyrelseMedlem>(styrelseMedlem); 
                    _util.SaveJsonAsFile(json,_util.PathToSpecialFolder() + styrelseMedlem.StyrelseMedlemId + ".json");

                    //Stäng login-fönster
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }

                    //Show the main window.
                    MainWindowViewModel wvm = new MainWindowViewModel();
                    MainWindow win = new MainWindow();
                    win.DataContext = wvm;
                    
                    win.MaxHeight = 400; ;
                    win.MaxWidth = 600;
                    win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    win.ShowDialog();                    

                }


            }
            else
            {
                ErrorMessage = "Fel lösenord eller Epostadress";
                var messageBox = new Wpf.Ui.Controls.MessageBox();
                messageBox.BorderThickness = new System.Windows.Thickness(2);
                var converter = new System.Windows.Media.BrushConverter();
                messageBox.BorderBrush = _util.ConvertColour("#000000"); 
                messageBox.Content = ErrorMessage;
                messageBox.Title = "Inloggningsfel";
                messageBox.CloseButtonText = "Stäng";
                messageBox.ShowDialogAsync();  
            }

        }  





              








    }
}
