using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VodovozSPB.Helpers {
    public class BaseViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged == null) 
                return;
            
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public enum Genders { Мужской = 1, Женский = 2 }
    }
}
