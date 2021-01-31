using System.Windows.Media.Imaging;

namespace ThesisApp.Models
{
    /// <summary>
    /// Data model which is used in progress reports for the Image test
    /// </summary>
    public class ImageTestReportModel
    {
        public BitmapImage ReportImage { get; set; } = null;
        public int ProgressArcAngle { get; set; } = 0;
    }
}
