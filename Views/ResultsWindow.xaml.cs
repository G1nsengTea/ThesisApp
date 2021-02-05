using System.Windows;
using ThesisApp.ViewModels;

namespace ThesisApp.Views
{
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();
            this.DataContext = new ResultsWindowViewModel();
        }
    }
}
