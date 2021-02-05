
namespace ThesisApp.Models
{
    /// <summary>
    /// Data model which defines data structure for results DataGrid
    /// </summary>
    public class ResultsDataModel
    {
        public string ExecutionType { get; set; }
        public string ImageTestTime { get; set; }
        public string PNumTestTime { get; set; }
        public string WebsitesTestTime { get; set; }
        public string TotalTime { get; set; }
        public string CompletitionStatus { get; set; }

        public ResultsDataModel(string executionType, string imageTestTime, string pNumTestTime, string websitesTestTime, string totalTime, string completetionStatus)
        {
            ExecutionType = executionType;
            ImageTestTime = imageTestTime;
            PNumTestTime = pNumTestTime;
            WebsitesTestTime = websitesTestTime;
            TotalTime = totalTime;
            CompletitionStatus = completetionStatus;
        }
    }
}
