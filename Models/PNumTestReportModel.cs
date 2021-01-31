namespace ThesisApp.Models
{
    /// <summary>
    /// Data model which is used in progress reports for the Prime Numbers test
    /// </summary>
    public class PNumTestReportModel
    {
        public long NthPNumberResult { get; set; }
        public long PNumberRangeResult { get; set; }
        public int ProgressArcAngle { get; set; } = 0;
    }
}
