﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.Services
{
    public interface IDialogWindow
    {
        bool? DialogResult { get; set; }
        object DataContext { get; set; }
        bool? ShowDialog();
    }
}
