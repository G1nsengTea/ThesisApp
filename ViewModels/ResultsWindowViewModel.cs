using System.Collections.ObjectModel;
using ThesisApp.BenchmarkClasses;
using ThesisApp.Models;

namespace ThesisApp.ViewModels
{
    /// <summary>
    /// This class is a ViewModel for the ResultsWindow.
    /// </summary>
    public class ResultsWindowViewModel : BaseViewModel
    {
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

        public ResultsWindowViewModel()
        {
            Results = new ObservableCollection<ResultsDataModel>(ResultsDataManager.GetResults());
        }
    }
}