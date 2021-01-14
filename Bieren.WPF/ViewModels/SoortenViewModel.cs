﻿using Bieren.WPF.Models;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    public class SoortenViewModel : WorkspaceViewModel
    {
        private IDataService _dataService;
        private BierSoort _selectedSoort;

        public ICommand AddSoortCommand { get; private set; }
        public ICommand UpdateSoortCommand { get; private set; }
        public ICommand DeleteSoortCommand { get; private set; }
        public ObservableCollection<BierSoort> Soorten { 
            get; private set; 
        }
        public BierSoort SelectedSoort
        {
            get
            {
                return _selectedSoort;
            }
            set
            {
                OnPropertyChanged(ref _selectedSoort, value);
            }

        }

        public SoortenViewModel(IDataService data)
        {
            _dataService = data;
            base.DisplayName = "Biersoorten";
            AddSoortCommand = new RelayCommand(VoegBierSoortToe);
            UpdateSoortCommand = new RelayCommand(WijzigBierSoort);
            DeleteSoortCommand = new RelayCommand(VerwijderBierSoort);
            Soorten = new ObservableCollection<BierSoort>(_dataService.GeefAlleBierSoorten());
        }

        private void VerwijderBierSoort()
        {
            if (SelectedSoort != null) return;
            _dataService.VerwijderBierSoort(SelectedSoort);
        }

        private void WijzigBierSoort()
        {
            if (SelectedSoort != null) return;
            _dataService.WijzigBierSoort(SelectedSoort);

        }

        private void VoegBierSoortToe()
        {
            BierSoort bierSoort = new BierSoort() { SoortNaam = "TESTBIER" };
            Soorten.Add(bierSoort);
            SelectedSoort = Soorten[Soorten.Count - 1];
            if (SelectedSoort != null) return;
            if(String.IsNullOrWhiteSpace(SelectedSoort.SoortNaam))
            {
                //To Boodschap geven naar gebruiker
                //Alert dialoogvenster tonen
                return;
            }
            //Biersoort toevoegen aan database;
            
            _dataService.VoegBierSoortToe(SelectedSoort);
        }
    }
    
}
