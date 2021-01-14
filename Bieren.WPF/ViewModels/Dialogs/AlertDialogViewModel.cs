using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;

namespace Bieren.WPF.ViewModels
{
    public class AlertDialogViewModel:DialogViewModelBase<DialogResults>
    {
        public ICommand OKCommand { get; private set; }
        public AlertDialogViewModel(string title, string message):base(title,message)
        {
            OKCommand = new RelayCommand<IDialogWindow>(OK);
        }
        private void OK(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResult);
        }
    }
}
