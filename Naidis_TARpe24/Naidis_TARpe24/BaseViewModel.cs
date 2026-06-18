using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Naidis_TARpe24
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nimetus = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nimetus));
    }
}
