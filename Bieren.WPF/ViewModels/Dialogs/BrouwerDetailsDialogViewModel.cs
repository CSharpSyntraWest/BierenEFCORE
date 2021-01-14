using Bieren.WPF.Models;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Bieren.WPF.ViewModels
{
    public class BrouwerDetailsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public Brouwer SelectedBrouwer { get; set; }
        public ICommand CloseCommand { get; private set; }
        public BrouwerDetailsDialogViewModel(string title, Brouwer brouwer) : base(title,  brouwer.BrNaam)
        {
            SelectedBrouwer = brouwer;
            CloseCommand = new RelayCommand<IDialogWindow>(Close);
        }
        private void Close(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResult);
        }
    }
}
