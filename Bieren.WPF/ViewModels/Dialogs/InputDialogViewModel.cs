using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Bieren.WPF.Services;
using Bieren.WPF.Utilities;

namespace Bieren.WPF.ViewModels
{
    public class InputDialogViewModel : DialogViewModelBase<object>
    {
        public ICommand OKCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public string _answer;


        public InputDialogViewModel(string title, string message) : base(title, message)
        {
            OKCommand = new RelayCommand<IDialogWindow>(Ok);
            CancelCommand = new RelayCommand<IDialogWindow>(Cancel);
        }
        public string Answer
        {
            get { return _answer; }
            set {
                OnPropertyChanged(ref _answer, value);
            }
        }
        private void Ok(IDialogWindow window)
        {
            CloseDialogWithResult(window, Answer);
        }

        private void Cancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Cancel);
        }
    }
}
