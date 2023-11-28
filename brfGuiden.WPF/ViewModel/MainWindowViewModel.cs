using brfGuiden.Core.Interface;
using brfGuiden.Models;
using brfGuiden.WPF.Helper;
using brfGuiden.WPF.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.ViewModel
{
    public partial class MainWindowViewModel: ObservableObject
    {

        
      
        private readonly ConfigHelper? _configHelper;

        public MainWindowViewModel()
        {
            _configHelper = new ConfigHelper();             
        }







        private string _formTitle= "BRFGUIDEN ";
        public string FormTitle
        {
            get 
            { 
                if (_configHelper!=null)
                {
                    _formTitle = _formTitle + "( " + _configHelper.ReadFromAppConfig("User")  + " )";
                }
                return _formTitle; 
            }
            set
            {
                if (_formTitle != value)
                {
                    _formTitle = value;
                    OnPropertyChanged(nameof(FormTitle));
                }
            }
        }



    }
}
