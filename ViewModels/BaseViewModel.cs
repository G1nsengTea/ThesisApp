using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThesisApp.ViewModels 
{
    /// <summary>
    /// Base ViewModel that implements INotifyPropertyChanged interface
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Implementation of PropertyChanged event from Microsoft documentation
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}