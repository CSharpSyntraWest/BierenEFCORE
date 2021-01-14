using Bieren.WPF.Models;
using Bieren.WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.ViewModels
{
    public class SoortenViewModel : WorkspaceViewModel
    {
        private IDataService _dataService;
        public IList<BierSoort> Soorten { get; private set; }

        public SoortenViewModel(IDataService data)
        {
            _dataService = data;
            base.DisplayName = "Biersoorten";

            Soorten = _dataService.GeefAlleBierSoorten();
        }
    }
    
}
