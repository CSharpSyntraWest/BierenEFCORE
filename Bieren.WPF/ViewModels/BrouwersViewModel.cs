
using Bieren.WPF.Models;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    public class BrouwersViewModel: WorkspaceViewModel
    {
        private IDialogService _dialogService;
        private IDataService _dataService;
        private ObservableCollection<Brouwer> _brouwers;
        private ObservableCollection<BierSoort> _bierSoorten;
        private Brouwer _selectedBrouwer;
        private DateTime _vanMarktDatum;
        private DateTime _totMarktDatum;
        public BrouwersViewModel(IDataService dataService,IDialogService dialogService)
        {
            base.DisplayName = "Brouwers";
            _dialogService = dialogService;
            _dataService = dataService;
            Brouwers = new ObservableCollection<Brouwer>(_dataService.GeefAlleBrouwers());
            BierSoorten = new ObservableCollection<BierSoort>(_dataService.GeefAlleBierSoorten());
            AddBrouwerCommand = new RelayCommand(VoegBrouwerToe);
            UpdateBrouwerCommand = new RelayCommand(WijzigBrouwerGegevens);
            DeleteBrouwerCommand = new RelayCommand(VerwijderBrouwer);
            ShowWebSiteDialogCommand = new RelayCommand(ShowWebSiteDialog);
            FilterOpMarktDatumCommand = new RelayCommand(FilterBierenOpMarktDatum);

            OphalenBierenVoorBrouwers();

            if (SelectedBrouwer != null)
            {
                VanMarktDatum = SelectedBrouwer.Bieren.Min(b => b.MarktDatum);
                TotMarktDatum = SelectedBrouwer.Bieren.Max(b => b.MarktDatum);
            }
        }
        public ICommand AddBrouwerCommand { get; private set; }
        public ICommand UpdateBrouwerCommand { get; private set; }
        public ICommand DeleteBrouwerCommand { get; private set; }
        private void FilterBierenOpMarktDatum()
        {
            SelectedBrouwer.Bieren = new ObservableCollection<Bier>(SelectedBrouwer.Bieren.Where(b => b.MarktDatum >= VanMarktDatum && b.MarktDatum <= TotMarktDatum).ToList());           
        }

        public DateTime VanMarktDatum
        {
            get { return _vanMarktDatum; }
            set { OnPropertyChanged(ref _vanMarktDatum, value); }
        }
        public DateTime TotMarktDatum
        {
            get { return _totMarktDatum; }
            set { OnPropertyChanged(ref _totMarktDatum, value); }
        }
        public ICommand FilterOpMarktDatumCommand { get; private set; }
        public ICommand ShowWebSiteDialogCommand { get; private set; }
        private void ShowWebSiteDialog()
        {
            System.Diagnostics.Process.Start("ie.exe www.google.be");
        }
        private void VerwijderBrouwer()
        {
            //Vraag bevestiging om bier te verwijderen via YesNo Dialoogvenster
            YesNoDialogViewModel dialog = new YesNoDialogViewModel("Verwijderen Brouwer", $"Bent u zeker dat u Brouwer '{SelectedBrouwer.BrNaam}' wilt verwijderen?");

            DialogResults result = _dialogService.OpenDialog(dialog);
            Debug.WriteLine(result);
            if (result == DialogResults.Yes)
            {
                Brouwers = new ObservableCollection<Brouwer>(_dataService.VerwijderBrouwer(SelectedBrouwer));
                if (_brouwers.Count > 0) SelectedBrouwer = _brouwers[0];
            }
        }

        private void WijzigBrouwerGegevens()
        {
            _dataService.WijzigBrouwer(SelectedBrouwer);
        }
        private void OphalenBierenVoorBrouwers()
        { 
            foreach(Brouwer brouwer in Brouwers)
            {
                brouwer.Bieren = new ObservableCollection<Bier>(_dataService.GeefBierenVoorBrouwer(brouwer));
            }
        }
        private void VoegBrouwerToe()
        {
            Brouwer brouwer = new Brouwer() { BrNaam = "Nieuwe Brouwer", Straat="NA", Gemeente="NA"  };
            Brouwers = new ObservableCollection<Brouwer>(_dataService.VoegBrouwerToe(brouwer));
            SelectedBrouwer = Brouwers[Brouwers.Count - 1];
        }
        public ObservableCollection<BierSoort> BierSoorten
        {
            get { return _bierSoorten; }
            set { OnPropertyChanged(ref _bierSoorten, value); }
        }
        public ObservableCollection<Brouwer> Brouwers
        {
            get { return _brouwers; }
            set { OnPropertyChanged(ref _brouwers, value); }
        }

        public Brouwer SelectedBrouwer
        {
            get { return _selectedBrouwer; }
            set {
                if(_selectedBrouwer !=null)  _selectedBrouwer.Bieren = new ObservableCollection<Bier>(_dataService.GeefBierenVoorBrouwer(value));
                OnPropertyChanged(ref _selectedBrouwer, value);
                if (SelectedBrouwer is null) return;
                if (SelectedBrouwer.Bieren.Count > 0)
                {
                    VanMarktDatum = SelectedBrouwer.Bieren.Min(b => b.MarktDatum);
                    TotMarktDatum = SelectedBrouwer.Bieren.Max(b => b.MarktDatum);
                }
            }
        }
    }
}
