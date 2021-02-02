using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using ThesisApp.Models;

namespace ThesisApp.BenchmarkClasses
{
    /// <summary>
    /// This class is used to implement the logic of the Websites test
    /// </summary>
    public static class WebsitesTest
    {
        //Execute Websites test in synchronous mode
        public static void StartSync(IProgress<WebsitesTestReportModel> progress)
        {
            //Create an instance of the report model object which will be later sent in progress report
            WebsitesTestReportModel report = new WebsitesTestReportModel();
            List<string> websites = PrepareWebsites();

            foreach (string site in websites)
            {
                //Cretae an instance of the website model which is later passed into the report
                WebsiteDataModel result = new WebsiteDataModel();

                DownloadWebsite(site);

                result.URI = site;
                result.isLoaded = true;
                report.LoadedWebsites.Add(result);
            }
            //send the report to update the UI
            report.ProgressArcAngle = 360;
            progress.Report(report);
        }

        //Execute Websites test in asynchronous mode
        public static async Task StartAsync(IProgress<WebsitesTestReportModel> progress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //Create an instance of the report model object which will be later sent in progress report
            WebsitesTestReportModel report = new WebsitesTestReportModel();
            List<string> websites = PrepareWebsites();
            int loadedWebsitesCount = 0;

            foreach (string site in websites)
            {
                //Cretae an instance of the website model which is later passed into the report
                WebsiteDataModel result = new WebsiteDataModel();

                await DownloadWebsiteAsync(site);

                result.URI = site;
                result.isLoaded = true;
                report.LoadedWebsites.Add(result);
                loadedWebsitesCount++;
                //Multiply by 360 because of arc shape of progress par
                report.ProgressArcAngle = (loadedWebsitesCount * 360) / websites.Count;
                progress.Report(report);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        //Execute Websites test in parallel mode
        public static void StartParallel(IProgress<WebsitesTestReportModel> progress)
        {
            //Create an instance of the report model object which will be later sent in progress report
            WebsitesTestReportModel report = new WebsitesTestReportModel();
            List<string> websites = PrepareWebsites();
            var temp = new object();

            Parallel.ForEach<string>(websites, (site, loopState) =>
            {
                //Cretae an instance of the website model which is later passed into the report
                WebsiteDataModel result = new WebsiteDataModel();

                DownloadWebsite(site);

                result.URI = site;
                result.isLoaded = true;
                lock (temp)
                {
                    report.LoadedWebsites.Add(result);
                }
            });
            //send the report to update the UI
            report.ProgressArcAngle = 360;
            progress.Report(report);
        }

        //This method is used to create a new webclient and download the website which is passed by the URL
        private static void DownloadWebsite(string URL)
        {
            WebClient client = new WebClient();
            client.DownloadString(URL);
        }

        //This method downloads everty website asynchronously
        private static async Task DownloadWebsiteAsync(string URL)
        {
            WebClient client = new WebClient();
            await client.DownloadStringTaskAsync(URL);
        }

        //Prepare a list of websites URLs
        private static List<string> PrepareWebsites()
        {
            List<string> results = new List<string>();

            results.Add("https://www.google.com");
            results.Add("https://www.youtube.com");
            results.Add("https://www.facebook.com");
            results.Add("https://visualstudio.microsoft.com/");
            results.Add("https://www.spotify.com");
            results.Add("https://www.twitch.tv");
            results.Add("https://www.reddit.com");
            results.Add("https://www.microsoft.com");
            results.Add("https://www.instagram.com");
            results.Add("https://stackoverflow.com");

            return results;
        }
    }
}
