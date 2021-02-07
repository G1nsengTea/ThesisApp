using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ThesisApp.BenchmarkClasses;
using ThesisApp.Models;

namespace ThesisApp.ViewModels
{
    /// <summary>
    /// This class is a ViewModel for the ResultsWindow.
    /// </summary>
    public class ResultsWindowViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<ResultsDataModel> _results = new ObservableCollection<ResultsDataModel>(); 
        private ObservableCollection<KeyValuePair<int, int>> _syncLS = new ObservableCollection<KeyValuePair<int, int>>();
        private ObservableCollection<KeyValuePair<int, int>> _asyncLS = new ObservableCollection<KeyValuePair<int, int>>();
        private ObservableCollection<KeyValuePair<int, int>> _parallelLS = new ObservableCollection<KeyValuePair<int, int>>();
        private ObservableCollection<KeyValuePair<int, int>> _parallelAsyncLS = new ObservableCollection<KeyValuePair<int, int>>();

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
        public ObservableCollection<KeyValuePair<int, int>> SyncLS
        {
            get
            {
                return _syncLS;
            }
            set
            {
                _syncLS = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<KeyValuePair<int, int>> AsyncLS
        {
            get
            {
                return _asyncLS;
            }
            set
            {
                _asyncLS = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<KeyValuePair<int, int>> ParallelLS
        {
            get
            {
                return _parallelLS;
            }
            set
            {
                _parallelLS = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<KeyValuePair<int, int>> ParallelAsyncLS
        {
            get
            {
                return _parallelAsyncLS;
            }
            set
            {
                _parallelAsyncLS = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ResultsWindowViewModel()
        {
            Results = new ObservableCollection<ResultsDataModel>(ResultsDataManager.GetResults());

            //Loop through all Results and populate ObservableCollections which are datasources for line chart
            int syncIndex = 1;
            int asyncIndex = 1;
            int parallelIndex = 1;
            int parallelAsyncIndex = 1;
            for (int i = 0; i < Results.Count; i++)
            {
                //Add to the chart only the results of runs which were successfuly completed (e.g. not cancelled with "Cancel Operation" button)
                if (Results[i].CompletitionStatus == "Completed")
                {
                    if (Results[i].ExecutionType == "Synchronous")
                        SyncLS.Add(new KeyValuePair<int, int>(syncIndex++, Int32.Parse(Results[i].TotalTime.Replace(":", ""))));

                    else if (Results[i].ExecutionType == "Asynchronous")
                        AsyncLS.Add(new KeyValuePair<int, int>(asyncIndex++, Int32.Parse(Results[i].TotalTime.Replace(":", ""))));

                    else if (Results[i].ExecutionType == "Parallel")
                        ParallelLS.Add(new KeyValuePair<int, int>(parallelIndex++, Int32.Parse(Results[i].TotalTime.Replace(":", ""))));

                    else if (Results[i].ExecutionType == "Parallel + Async")
                        ParallelAsyncLS.Add(new KeyValuePair<int, int>(parallelAsyncIndex++, Int32.Parse(Results[i].TotalTime.Replace(":", ""))));
                }
            }
        }
    }
}