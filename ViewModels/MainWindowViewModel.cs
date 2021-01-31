using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ThesisApp.BenchmarkClasses;
using ThesisApp.Models;

namespace ThesisApp.ViewModels
{
    /// <summary>
    /// This class is a ViewModel for the MainWindow.
    /// It is responsible for all UI updates
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Local fields that are only used in this ViewModel
        /// </summary>
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly int pNumRangeTest = 400000;
        private static readonly int pNumNthTest = 400000;
        private readonly Stopwatch ImageTestWatch = new Stopwatch();
        private readonly Stopwatch PNumTestWatch = new Stopwatch();
        private readonly Stopwatch WebsitesTestWatch = new Stopwatch();
        private readonly Stopwatch TotalTimeWatch = new Stopwatch();
        private readonly DispatcherTimer DTime = new DispatcherTimer();
        private CancellationTokenSource cts = new CancellationTokenSource();
        #endregion

        /// <summary>
        /// Following are the properties that are binded to the UI elements
        /// </summary>
        #region Properties
        #region Private Part
        private BitmapImage _imageTestSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/TestImageBW.jpg"));
        private BitmapImage _googleLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/GoogleLogoBW.png"));
        private BitmapImage _youTubeLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/YouTubeLogoBW.png"));
        private BitmapImage _faceBookLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/FacebookLogoBW.png"));
        private BitmapImage _vSLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/VSLogoBW.png"));
        private BitmapImage _spotifyLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/SpotifyLogoBW.png"));
        private BitmapImage _twitchLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/TwitchLogoBW.png"));
        private BitmapImage _redditLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/RedditLogoBW.png"));
        private BitmapImage _microsoftLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/MicrosoftLogoBW.png"));
        private BitmapImage _instagramLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/InstagramLogoBW.png"));
        private BitmapImage _stackLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/StackLogoBW.png"));
        private int _imageTestArcAngle = 0;
        private int _pNumTestArcAngle = 0;
        private int _websitesTestArcAngle = 0;
        private Visibility _imageTestCheckmarkVisibility = Visibility.Hidden;
        private Visibility _pNumTestCheckmarkVisibility = Visibility.Hidden;
        private Visibility _websitesTestCheckmarkVisibility = Visibility.Hidden;
        private Visibility _imageTestCrossmarkVisibility = Visibility.Hidden;
        private Visibility _pNumTestCrossmarkVisibility = Visibility.Hidden;
        private Visibility _websitesTestCrossmarkVisibility = Visibility.Hidden;
        private string _pNumberRangeText = $"There are X prime numbers between 2 and {pNumRangeTest}";
        private string _pNumberNthText = $"X is {pNumNthTest}th prime number";
        private string _imageTestTimerText = "00:00:00";
        private string _pNumTestTimerText = "00:00:00";
        private string _websitesTestTimerText = "00:00:00";
        private string _totalTimeText = "00:00:00";
        private string _asyncBtnTag;
        private string _cancelBtnTag;
        private bool _syncBtnIsEnabled = true;
        private bool _asyncBtnIsEnabled = true;
        private bool _parallelBtnIsEnabled = false;
        private bool _parallelAsyncBtnIsEnabled;
        private bool _resetBtnIsEnabled = false;
        private bool _cancelBthIsEnabled = false;
        private Brush _imageTestProgressBarColour = new SolidColorBrush(Colors.LightGreen);
        private Brush _pNumTestProgressBarColour = new SolidColorBrush(Colors.LightGreen);
        private Brush _websitesTestProgressBarColour = new SolidColorBrush(Colors.LightGreen);
        #endregion

        #region Public Part
        public string PNumberRangeText
        {
            get { return _pNumberRangeText; }
            set
            {
                _pNumberRangeText = value;
                OnPropertyChanged();
            }
        }
        public string PNumberNthText
        {
            get { return _pNumberNthText; }
            set
            {
                _pNumberNthText = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage ImageTestSource
        {
            get { return _imageTestSource; }
            set
            {
                _imageTestSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage GoogleLogoSource
        {
            get { return _googleLogoSource; }
            set
            {
                _googleLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage YouTubeLogoSource
        {
            get { return _youTubeLogoSource; }
            set
            {
                _youTubeLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage FaceBookLogoSource
        {
            get { return _faceBookLogoSource; }
            set
            {
                _faceBookLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage VSLogoSource
        {
            get { return _vSLogoSource; }
            set
            {
                _vSLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage SpotifyLogoSource
        {
            get { return _spotifyLogoSource; }
            set
            {
                _spotifyLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage TwitchLogoSource
        {
            get { return _twitchLogoSource; }
            set
            {
                _twitchLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage RedditLogoSource
        {
            get { return _redditLogoSource; }
            set
            {
                _redditLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage MicrosoftLogoSource
        {
            get { return _microsoftLogoSource; }
            set
            {
                _microsoftLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage InstagramLogoSource
        {
            get { return _instagramLogoSource; }
            set
            {
                _instagramLogoSource = value;
                OnPropertyChanged();
            }
        }
        public BitmapImage StackLogoSource
        {
            get { return _stackLogoSource; }
            set
            {
                _stackLogoSource = value;
                OnPropertyChanged();
            }
        }
        public int ImageTestArcAngle
        {
            get { return _imageTestArcAngle; }
            set
            {
                _imageTestArcAngle = value;
                OnPropertyChanged();
            }
        }
        public int PNumTestArcAngle
        {
            get { return _pNumTestArcAngle; }
            set
            {
                _pNumTestArcAngle = value;
                OnPropertyChanged();
            }
        }
        public int WebsitesTestArcAngle
        {
            get { return _websitesTestArcAngle; }
            set
            {
                _websitesTestArcAngle = value;
                OnPropertyChanged();
            }
        }
        public Visibility ImageTestCheckmarkVisibility
        {
            get { return _imageTestCheckmarkVisibility; }
            set
            {
                _imageTestCheckmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility PNumTestCheckmarkVisibility
        {
            get { return _pNumTestCheckmarkVisibility; }
            set
            {
                _pNumTestCheckmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility WebsitesTestCheckmarkVisibility
        {
            get { return _websitesTestCheckmarkVisibility; }
            set
            {
                _websitesTestCheckmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility ImageTestCrossmarkVisibility
        {
            get { return _imageTestCrossmarkVisibility; }
            set
            {
                _imageTestCrossmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility PNumTestCrossmarkVisibility
        {
            get { return _pNumTestCrossmarkVisibility; }
            set
            {
                _pNumTestCrossmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility WebsitesTestCrossmarkVisibility
        {
            get { return _websitesTestCrossmarkVisibility; }
            set
            {
                _websitesTestCrossmarkVisibility = value;
                OnPropertyChanged();
            }
        }
        public string ImageTestTimerText
        {
            get { return _imageTestTimerText; }
            set
            {
                _imageTestTimerText = value;
                OnPropertyChanged();
            }
        }
        public string PNumTestTimerText
        {
            get { return _pNumTestTimerText; }
            set
            {
                _pNumTestTimerText = value;
                OnPropertyChanged();
            }
        }
        public string WebsitesTestTimerText
        {
            get { return _websitesTestTimerText; }
            set
            {
                _websitesTestTimerText = value;
                OnPropertyChanged();
            }
        }
        public string TotalTimeText
        {
            get { return _totalTimeText; }
            set
            {
                _totalTimeText = value;
                OnPropertyChanged();
            }
        }
        public string AsyncBtnTag
        {
            get { return _asyncBtnTag; }
            set
            {
                _asyncBtnTag = value;
                OnPropertyChanged();
            }
        }
        public string CancelBtnTag
        {
            get { return _cancelBtnTag; }
            set
            {
                _cancelBtnTag = value;
                OnPropertyChanged();
            }
        }
        public bool SyncBtnIsEnabled
        {
            get { return _syncBtnIsEnabled; }
            set
            {
                _syncBtnIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool AsyncBtnIsEnabled
        {
            get { return _asyncBtnIsEnabled; }
            set
            { 
                _asyncBtnIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool ParallelBtnIsEnabled
        {
            get { return _parallelBtnIsEnabled; }
            set
            { 
                _parallelBtnIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool ParallelAsyncBtnIsEnabled
        {
            get { return _parallelAsyncBtnIsEnabled; }
            set
            {
                _parallelAsyncBtnIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool ResetBtnIsEnabled
        {
            get { return _resetBtnIsEnabled; }
            set
            { 
                _resetBtnIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool CancelBtnIsEnabled
        {
            get { return _cancelBthIsEnabled; }
            set
            {
                _cancelBthIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public Brush ImageTestProgressBarColour
        {
            get { return _imageTestProgressBarColour; }
            set 
            {
                _imageTestProgressBarColour = value;
                OnPropertyChanged();
            }
        }
        public Brush PNumTestProgressBarColour
        {
            get { return _pNumTestProgressBarColour; }
            set
            {
                _pNumTestProgressBarColour = value;
                OnPropertyChanged();
            }
        }
        public Brush WebsitesTestProgressBarColour
        {
            get { return _websitesTestProgressBarColour; }
            set
            {
                _websitesTestProgressBarColour = value;
                OnPropertyChanged();
            }
        }


        #endregion
        #endregion

        public MainWindowViewModel()
        {
            DTime.Interval = TimeSpan.FromMilliseconds(1);
            DTime.Tick += UpdateTimers;
        }

        /// <summary>
        /// Commands that are binded to the buttons.
        /// When button is clicked, function that is passed
        /// via the RelayCommand in the get part of the property
        /// is executed.
        /// </summary>
            #region Commands
            #region Private Part
        private ICommand _syncTestCommand;
        private ICommand _asyncTestCommand;
        private ICommand _resetCommand;
        private ICommand _helpCommand;
        private ICommand _exitCommand;
        private ICommand _cancelCommand;
        #endregion

        #region Public Part
        public ICommand SyncTestCommand
        {
            get 
            {
                if (_syncTestCommand == null)
                {
                    _syncTestCommand = new RelayCommand(
                        obj => SyncTest(),
                        obj => CanSyncTest()
                        );
                }
                return _syncTestCommand;
            }
        }
        public ICommand AsyncTestCommand
        {
            get
            {
                if (_asyncTestCommand == null)
                {
                    _asyncTestCommand = new RelayCommand(
                        obj => AsyncTest(),
                        obj => CanAsyncTest()
                        );
                }
                return _asyncTestCommand;
            }
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(
                        obj => ResetUI(),
                        obj => CanReset()
                        );
                }
                return _resetCommand;
            }
        }
        public ICommand HelpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new RelayCommand(
                        obj => Help(),
                        obj => { return true; }
                        );
                }
                return _helpCommand;
            }
        }
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(
                        obj => Exit(),
                        obj => { return true; }
                        );
                }
                return _exitCommand;
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                        obj => Cancel(),
                        obj => CanCancel()
                        );
                }
                return _cancelCommand;
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Following are functions that are invoked by Commands when a button is clicked
        /// </summary>
        /// <returns></returns>
        #region Functions for buttons

        /// <summary>
        /// These are passed to the Commands and are used to check if the button could be clicked or not
        /// </summary>
        #region Permission Checks
        private bool CanSyncTest()
        {
            return SyncBtnIsEnabled;
        }
        private bool CanAsyncTest()
        {
            return AsyncBtnIsEnabled;
        }
        private bool CanReset()
        {
            return ResetBtnIsEnabled;
        }
        private bool CanCancel()
        {
            return CancelBtnIsEnabled;
        }
        #endregion

        /// <summary>
        /// Following are the functions that contain the logic that is executed once a button is pressed
        /// </summary>
        #region Buttons Logic
        private void SyncTest()
        {
            TotalTimeWatch.Start();

            //Initialize instances of Progress class that are later used to trigger
            //the events that update UI
            Progress<ImageTestReportModel> imageTestProgress = new Progress<ImageTestReportModel>();
            imageTestProgress.ProgressChanged += ImageTestProgressEvent;
            Progress<PNumTestReportModel> pNumTestProgress = new Progress<PNumTestReportModel>();
            pNumTestProgress.ProgressChanged += PNumTestProgressEvent;
            Progress<WebsitesTestReportModel> websitesTestProgress = new Progress<WebsitesTestReportModel>();
            websitesTestProgress.ProgressChanged += WebsitesTestProgressEvent;

            //Start executing each test sequentially
            ImageTestWatch.Start();
            ImageTest.StartSync(imageTestProgress);
            ImageTestWatch.Stop();
            ImageTestCheckmarkVisibility = Visibility.Visible;

            PNumTestWatch.Start();
            PrimeNumbersTest.StartSync(pNumTestProgress, pNumRangeTest, pNumNthTest);
            PNumTestWatch.Start();
            PNumTestCheckmarkVisibility = Visibility.Visible;

            WebsitesTestWatch.Start();
            WebsitesTest.StartSync(websitesTestProgress);
            WebsitesTestWatch.Stop();
            WebsitesTestCheckmarkVisibility = Visibility.Visible;

            //Enable Reset button to let the user observe the results
            //and then reset UI before next run
            SyncBtnIsEnabled = false;
            ResetBtnIsEnabled = true;

            //Stop Total timer and update all timers
            TotalTimeWatch.Stop();
            UpdateTimers(this, new EventArgs());
        }
        private async void AsyncTest()
        {
            //disable UI elements
            ResetUI();
            SyncBtnIsEnabled = AsyncBtnIsEnabled = ParallelBtnIsEnabled = ParallelAsyncBtnIsEnabled = false;
            CancelBtnIsEnabled = true;
            AsyncBtnTag = "Clicked";

            DTime.Start();
            TotalTimeWatch.Start();

            //Initialize instances of Progress class that are later used to trigger
            //the events that update UI
            Progress<ImageTestReportModel> imageTestProgress = new Progress<ImageTestReportModel>();
            imageTestProgress.ProgressChanged += ImageTestProgressEvent;
            Progress<PNumTestReportModel> pNumTestProgress = new Progress<PNumTestReportModel>();
            pNumTestProgress.ProgressChanged += PNumTestProgressEvent;
            Progress<WebsitesTestReportModel> websitesTestProgress = new Progress<WebsitesTestReportModel>();
            websitesTestProgress.ProgressChanged += WebsitesTestProgressEvent;


            ImageTestWatch.Start();
            //Use try catch statement for cancellation Tokens
            try
            {
                if (!cts.IsCancellationRequested)
                {
                    await ImageTest.StartAsync(imageTestProgress, cts.Token);
                    ImageTestCheckmarkVisibility = Visibility.Visible;
                }
            }
            catch (OperationCanceledException)
            {
                ImageTestProgressBarColour = new SolidColorBrush(Colors.Red);
                ImageTestCrossmarkVisibility = Visibility.Visible;
                CancelBtnTag = null;
            }
            ImageTestWatch.Stop();

            PNumTestWatch.Start();
            try
            {
                if (!cts.IsCancellationRequested)
                {
                    await PrimeNumbersTest.StartAsync(pNumTestProgress, pNumRangeTest, pNumNthTest, cts.Token);
                    PNumTestCheckmarkVisibility = Visibility.Visible;
                }
            }
            catch (OperationCanceledException) 
            {
                PNumTestProgressBarColour = new SolidColorBrush(Colors.Red);
                PNumTestCrossmarkVisibility = Visibility.Visible;
                CancelBtnTag = null;
            }
            PNumTestWatch.Stop();

            WebsitesTestWatch.Start();
            try
            {
                if (!cts.IsCancellationRequested)
                {
                    await WebsitesTest.StartAsync(websitesTestProgress, cts.Token);
                    WebsitesTestCheckmarkVisibility = Visibility.Visible;
                }
            }
            catch (OperationCanceledException)
            {
                WebsitesTestProgressBarColour = new SolidColorBrush(Colors.Red);
                WebsitesTestCrossmarkVisibility = Visibility.Visible;
                CancelBtnTag = null;
            }
            WebsitesTestWatch.Stop();

            //enable UI elements
            AsyncBtnIsEnabled = ParallelAsyncBtnIsEnabled = ResetBtnIsEnabled = true;
            CancelBtnIsEnabled = false;
            AsyncBtnTag = null;

            //Force update UI to enable disabled buttons
            CommandManager.InvalidateRequerySuggested();

            TotalTimeWatch.Stop();
            DTime.Stop();
        }
        private void Help()
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }
        private void Exit()
        {
            Application.Current.Shutdown();
        }
        //This function is used to reset all UI elements to their default states
        private void ResetUI()
        {
            SyncBtnIsEnabled = true;
            ResetBtnIsEnabled = false;
            ImageTestTimerText = PNumTestTimerText = WebsitesTestTimerText = TotalTimeText = "00:00:00";
            ImageTestWatch.Reset();
            PNumTestWatch.Reset();
            WebsitesTestWatch.Reset();
            TotalTimeWatch.Reset();
            PNumberRangeText = $"There are X prime numbers between 2 and {pNumRangeTest}";
            PNumberNthText = $"X is {pNumNthTest}th prime number";
            ImageTestArcAngle = PNumTestArcAngle = WebsitesTestArcAngle = 0;
            ImageTestProgressBarColour = PNumTestProgressBarColour = WebsitesTestProgressBarColour = new SolidColorBrush(Colors.LightGreen);
            ImageTestCheckmarkVisibility = PNumTestCheckmarkVisibility = WebsitesTestCheckmarkVisibility =
                ImageTestCrossmarkVisibility = PNumTestCrossmarkVisibility = WebsitesTestCrossmarkVisibility = Visibility.Hidden;
            ImageTestSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/TestImageBW.jpg"));
            GoogleLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/GoogleLogoBW.png"));
            YouTubeLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/YouTubeLogoBW.png"));
            FaceBookLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/FacebookLogoBW.png"));
            VSLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/VSLogoBW.png"));
            SpotifyLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/SpotifyLogoBW.png"));
            TwitchLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/TwitchLogoBW.png"));
            RedditLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/RedditLogoBW.png"));
            MicrosoftLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/MicrosoftLogoBW.png"));
            InstagramLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/instagramLogoBW.png"));
            StackLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/StackLogoBW.png"));
            if (cts.IsCancellationRequested)
            {
                cts.Dispose();
                cts = new CancellationTokenSource();
            }
        }
        private void Cancel()
        {
            CancelBtnIsEnabled = false;
            CancelBtnTag = "Clicked";
            cts.Cancel();
        }
        #endregion
        #endregion

        /// <summary>
        /// Events are following
        /// </summary>
        #region Events
        //This event is called whenever the timers must be updated
        //During asynchronous execution event is called every millisecond
        private void UpdateTimers(object sender, EventArgs e)
        {
            TimeSpan ts = ImageTestWatch.Elapsed;
            TimeSpan ts2 = PNumTestWatch.Elapsed;
            TimeSpan ts3 = WebsitesTestWatch.Elapsed;
            TimeSpan totalTs = TotalTimeWatch.Elapsed;

            ImageTestTimerText = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            PNumTestTimerText = String.Format("{0:00}:{1:00}:{2:00}", ts2.Minutes, ts2.Seconds, ts2.Milliseconds / 10);
            WebsitesTestTimerText = String.Format("{0:00}:{1:00}:{2:00}", ts3.Minutes, ts3.Seconds, ts3.Milliseconds / 10);
            TotalTimeText = String.Format("{0:00}:{1:00}:{2:00}", totalTs.Minutes, totalTs.Seconds, totalTs.Milliseconds / 10);
        }

        //Following are events that are used to update the UI
        private void ImageTestProgressEvent(object sender, ImageTestReportModel e)
        {
            if (e.ReportImage != null)
                ImageTestSource = e.ReportImage;
            ImageTestArcAngle = e.ProgressArcAngle;
        }
        private void PNumTestProgressEvent(object sender, PNumTestReportModel e)
        {
            if (e.PNumberRangeResult > 0)
                PNumberRangeText = $"There are {e.PNumberRangeResult} prime numbers between 2 and {pNumRangeTest}";
            if (e.NthPNumberResult > 0)
                PNumberNthText = $"{e.NthPNumberResult} is {pNumNthTest}th prime number";
            PNumTestArcAngle = e.ProgressArcAngle;
        }
        private void WebsitesTestProgressEvent(object sender, WebsitesTestReportModel e)
        {
            WebsitesTestArcAngle = e.ProgressArcAngle;
            foreach (var site in e.LoadedWebsites)
            {
                if (site.URI.Contains("google") && site.isLoaded)
                    GoogleLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/GoogleLogoColorized.png"));
                else if (site.URI.Contains("youtube") && site.isLoaded)
                    YouTubeLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/YouTubeLogoColorized.png"));
                else if (site.URI.Contains("facebook") && site.isLoaded)
                    FaceBookLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/FacebookLogoColorized.png"));
                else if (site.URI.Contains("visualstudio") && site.isLoaded)
                    VSLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/VSLogoColorized.png"));
                else if (site.URI.Contains("spotify") && site.isLoaded)
                    SpotifyLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/SpotifyLogoColorized.png"));
                else if (site.URI.Contains("twitch") && site.isLoaded)
                    TwitchLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/TwitchLogoColorized.png"));
                else if (site.URI.Contains("reddit") && site.isLoaded)
                    RedditLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/RedditLogoColorized.png"));
                else if (site.URI.Contains("microsoft") && site.isLoaded)
                    MicrosoftLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/MicrosoftLogoColorized.png"));
                else if (site.URI.Contains("instagram") && site.isLoaded)
                    InstagramLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/instagramLogoColorized.png"));
                else if (site.URI.Contains("stackoverflow") && site.isLoaded)
                    StackLogoSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Images/Logos/StackLogoColorized.png"));
            }
        }

        //Implementation of PropertyChanged event from Microsoft documentation
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

    }
}
