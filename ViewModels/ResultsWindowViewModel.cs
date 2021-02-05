using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThesisApp.BenchmarkClasses;
using ThesisApp.Models;

namespace ThesisApp.ViewModels
{
    /// <summary>
    /// This class is a ViewModel for the ResultsWindow.
    /// </summary>
    class ResultsWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        private ObservableCollection<ResultsDataModel> _results = new ObservableCollection<ResultsDataModel>(); 

        public ObservableCollection<ResultsDataModel> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ResultsWindowViewModel()
        {
            Results = new ObservableCollection<ResultsDataModel>(ResultsDataManager.GetResults());
        }

        #region Events
        //Implementation of PropertyChanged event from Microsoft documentation
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
