using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using ThesisApp.Models;
using System.Collections.Concurrent;

namespace ThesisApp.BenchmarkClasses
{
    /// <summary>
    /// This class is used to implement the logic of the Image test
    /// </summary>
    public static class ImageTest
    {
        private static readonly Random rnd = new Random();

        //Bitmaps to store colorized and desaturated versions of the Image
        private static readonly Bitmap ColorizedImage = new Bitmap(Properties.Resources.TestImageColorized);
        private static readonly Bitmap BWImage = new Bitmap(Properties.Resources.TestImageBW);
        private static Bitmap ResultImg = new Bitmap(BWImage.Width, BWImage.Height);
        //Array to reference the coordinates of pixels
        private static readonly int[][] ColorCoordinates = new int[ColorizedImage.Width * ColorizedImage.Height][];
        //Set how frequently the report is sent to the UI
        private static readonly int ProgressReportFrequency = 20;

        //Execute Image test in synchronous mode. The same method is called for parallel mode.
        public static void StartSync(IProgress<ImageTestReportModel> progress)
        {
            //Create an instance of the report model object which will be later sent in progress report
            ImageTestReportModel report = new ImageTestReportModel();            
            ResultImg = BWImage;
            SetCooridnates();
                        
            for (int i = 0; i < ColorCoordinates.Length; i++)
            {
                ResultImg.SetPixel(ColorCoordinates[i][0], ColorCoordinates[i][1], ColorizedImage.GetPixel(ColorCoordinates[i][0], ColorCoordinates[i][1]));
            }

            //Report the progress to update the UI
            report.ReportImage = ToWpfBitmap(ResultImg);
            report.ProgressArcAngle = 360;
            progress.Report(report);
        }
        //Execute Image test in asynchronous mode
        public static async Task StartAsync(IProgress<ImageTestReportModel> progress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //Create an instance of the report model object which will be later sent in progress report
            ImageTestReportModel report = new ImageTestReportModel();
            ResultImg = new Bitmap(Properties.Resources.TestImageBW);
            int reportIndex = 0;

            //Send initial progress report
            reportIndex++;
            report.ProgressArcAngle = (reportIndex * 360) / ProgressReportFrequency;
            progress.Report(report);

            await Task.Run(() => SetCooridnates());
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Run(() =>
            {
                for (int i = 0; i < ColorCoordinates.Length; i++)
                {
                    ResultImg.SetPixel(ColorCoordinates[i][0], ColorCoordinates[i][1], ColorizedImage.GetPixel(ColorCoordinates[i][0], ColorCoordinates[i][1]));

                    //Send the report to the UI thread to update progress bar
                    if (i % (ColorCoordinates.Length / ProgressReportFrequency ) == 0 && i > 0 || i == ColorCoordinates.Length - 1)
                    {
                        reportIndex++;
                        report.ReportImage = ToWpfBitmap(ResultImg);
                        //Adjust the value of progress bar angle (multiply by 343 instead of 360)
                        //because the first report was already sent earlier
                        report.ProgressArcAngle = (reportIndex * 343) / ProgressReportFrequency;
                        progress.Report(report);
                    }

                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            });
            cancellationToken.ThrowIfCancellationRequested();
        }        
        
        //This function is used to populate and shuffle to coordinates array
        private static void SetCooridnates()
        {
            if (ColorCoordinates.Length > 0)
                Array.Clear(ColorCoordinates, 0, ColorCoordinates.Length);

            //Populate the array with coordinates corresponding to the pixels on the Image
            for (int i = 0, a = 0; i < ColorizedImage.Width; i++)
            {
                for (int j = 0; j < ColorizedImage.Height; j++, a++)
                {
                    ColorCoordinates[a] = new int[] { i, j };
                }
            }

            //Shuffle the coordinates array
            int n = ColorCoordinates.Length;
            while (n > 0)
            {
                int k = rnd.Next(n--);
                var temp = ColorCoordinates[k];
                ColorCoordinates[k] = ColorCoordinates[n];
                ColorCoordinates[n] = temp;
            }
        }

        //This method is used to covert Bitmap into BitmapImage which is later displayed in the application
        private static BitmapImage ToWpfBitmap(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
