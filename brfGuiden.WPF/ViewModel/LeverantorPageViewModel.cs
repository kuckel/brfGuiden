using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.ViewModel
{
    public partial class LeverantorPageViewModel: ObservableObject
    {
        private ObservableCollection<Leverantor>? _leverantorer { get; set; }
        private readonly ILeverantorService? _leverantorService;

        public LeverantorPageViewModel(ILeverantorService leverantorservice)
        {
            _leverantorService=leverantorservice;
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
