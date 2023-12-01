using brfGuiden.Core.Interface;
using brfGuiden.Core.Service;
using brfGuiden.Models;
using brfGuiden.WPF.Interface; 
using brfGuiden.WPF.Service;
using CommunityToolkit.Mvvm.ComponentModel;


namespace brfGuiden.WPF.ViewModel
{
    public partial class MedlemPageViewModel: ObservableObject
    {
        private readonly IForeningService? _foreningService;


        public MedlemPageViewModel(IForeningService foreningservice) 
        {

            _foreningService= foreningservice;
            Forening forening = _foreningService.GetForening();    

        }

    }
}



