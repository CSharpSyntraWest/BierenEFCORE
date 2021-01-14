using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.Services
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> viewModel);
    }
}
