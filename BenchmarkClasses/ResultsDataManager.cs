using System.Collections.Generic;
using ThesisApp.Models;

namespace ThesisApp.BenchmarkClasses
{
    /// <summary>
    /// Simple manager to store and access reults of benchmark executions.
    /// </summary>
    public static class ResultsDataManager
    {
        private static readonly List<ResultsDataModel> Results = new List<ResultsDataModel>();

        public static void SetResults(string executionType, string imageTestTime, string pNumTestTime, string websitesTestTime, string totalTime, bool isCompleted)
        {
            if (isCompleted)
                Results.Add(new ResultsDataModel(executionType, imageTestTime, pNumTestTime, websitesTestTime, totalTime, "Completed"));
            else
                Results.Add(new ResultsDataModel(executionType, imageTestTime, pNumTestTime, websitesTestTime, totalTime, "Cancelled"));
        }

        public static List<ResultsDataModel> GetResults()
        {
            return Results;
        }
    }
}
