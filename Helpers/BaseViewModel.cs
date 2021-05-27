﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using VodovozSPB.Data;

namespace VodovozSPB.Helpers {
    class BaseViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        protected void NotifyPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }         
    }
}
