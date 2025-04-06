using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pizza_App.ViewModels
{
    // BaseViewModel implements INotifyPropertyChanged to support data binding.
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies the UI that a property value has changed.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
