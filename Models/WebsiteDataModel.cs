namespace ThesisApp.Models
{
    /// <summary>
    /// Data model which is passed as one of the arguments in progress report for Websites test
    /// It is later used to colorize the icons of websites that finished loading
    /// </summary>
    public class WebsiteDataModel
    {
        public string URI = null;
        public bool isLoaded = false;
    }
}
