using brfGuiden.Models;
using brfGuiden.WPF.Helper;
using brfGuiden.WPF.Interface;
using brfGuiden.WPF.Models;
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
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace brfGuiden.WPF.ViewModel
{
    public partial class EditLeverantorPageViewModel : ObservableObject
    {
        private ObservableCollection<Betyg>? _betygLista { get; set; }
        private readonly ILeverantorService? _leverantorService;
        private Leverantor? _leverantor;
        private readonly Util? _util;
       


        public EditLeverantorPageViewModel(Leverantor selectedLeverantor) 
        {
            _leverantor= selectedLeverantor;
            _leverantorService = App.ServiceProvider.GetService<ILeverantorService>();  
            _util = new Util();
            _betygLista = new ObservableCollection<Betyg>();
            _betygLista.Add(new Betyg { BetygText = "Ange ett betyg för leverantören", BetygValue = 0 });
            _betygLista.Add(new Betyg { BetygText = "Undvik denna leverantör", BetygValue = 1 });
            _betygLista.Add(new Betyg { BetygText = "Dålig leverantör", BetygValue = 2 });
            _betygLista.Add(new Betyg { BetygText = "Medelmåttig leverantör", BetygValue = 3 });
            _betygLista.Add(new Betyg { BetygText = "Bra leverantör", BetygValue = 4 });
            _betygLista.Add(new Betyg { BetygText = "Utmärkt leverantör", BetygValue = 5 });

        }

            
            



        public ObservableCollection<Betyg> BetygsLista
        {
            get { return _betygLista; }
            set
            {
                if (_betygLista != value)
                {
                    _betygLista = value;
                    OnPropertyChanged(nameof(BetygsLista));
                }
            }
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



        [RelayCommand ]
        public void Update()
        {

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Leverantor, serviceProvider: null, items: null);

            if (Validator.TryValidateObject(Leverantor, validationContext, validationResults, validateAllProperties: true) && _leverantor != null)
            {

                Leverantor newLev = _leverantorService.UpdateLeverantor(_leverantor);
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
                    ShowError("Ett fel uppstod när leverantören skulle uppdateras");
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


        private int _betyg;
        public int Betyg
        {
            get { return _betyg; }
            set
            {
                if (_betyg != value)
                {
                    _betyg = value;
                    OnPropertyChanged(nameof(Betyg));
                }
            }
        }


    }
}
