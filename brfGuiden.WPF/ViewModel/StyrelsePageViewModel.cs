using brfGuiden.Core.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.ViewModel
{
    public partial class StyrelsePageViewModel : ObservableObject
    {

        private readonly IStyrelseService _styrelseService;


        public StyrelsePageViewModel(IStyrelseService styrelseservice)
        {
            _styrelseService=styrelseservice;
        }



    }
}
