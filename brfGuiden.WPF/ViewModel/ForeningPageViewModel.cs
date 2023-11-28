using CommunityToolkit.Mvvm.ComponentModel;
using System;

using System.ComponentModel;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using brfGuiden.Core.Interface;
using brfGuiden.WPF.ViewModel;

namespace brfGuiden.WPF.ViewModel
{
    public partial class ForeningPageViewModel : ObservableObject
    {
        //private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        //public MainWindowViewModel MainVM => ViewModelLocator.Instance.MainViewModel;
        //public string this[string columnName] => throw new NotImplementedException();
        //public string Error => throw new NotImplementedException();
        private readonly IForeningService? _foreningService;


        public ForeningPageViewModel(IForeningService foreningservice)
        {
            _foreningService = foreningservice;
            //_forening = _foreningService.GetForening();
        }

        public string Error
        {
            get { return null; }
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

        [ObservableProperty]
        public Forening? _forening;

        private string? _organizationNumber;
        [Required(ErrorMessage = "Fältet är obligatoriskt.")]
        [MinLength(12, ErrorMessage = "OrgNr måste bestå av 12 tecken")]
        [MaxLength(12, ErrorMessage = "OrgNr måste bestå av max 12 tecken")]
        public string OrganizationNumber
        {
            get { return _organizationNumber ?? ""; }
            set
            {
                if (_organizationNumber != value)
                {
                    _organizationNumber = value;
                    OnPropertyChanged(nameof(OrganizationNumber));
                }
            }
        }


        private string? _postNr;
        [Required(ErrorMessage = "Fältet är obligatoriskt.")]
        [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "Felaktigt format i PostNr.")]
        [MaxLength(6, ErrorMessage = "Max antal tecken är 6")]
        [MinLength(5, ErrorMessage = "Fel antal tecken i PostNr")]
        public string PostNr
        {
            get { return _postNr ?? ""; }
            set
            {
                if (_postNr != value)
                {
                    _postNr = value;
                    OnPropertyChanged(nameof(PostNr));
                }
            }
        }



    }
}
