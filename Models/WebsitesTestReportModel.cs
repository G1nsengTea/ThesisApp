using System.Collections.Generic;

namespace ThesisApp.Models
{
    /// <summary>
    /// Data model which is used in progress reports for the Websites test
    /// </summary>
    public class WebsitesTestReportModel
    {
        public int ProgressArcAngle { get; set; } = 0;
        public List<WebsiteDataModel> LoadedWebsites { get; set; } = new List<WebsiteDataModel>();
    }
}
