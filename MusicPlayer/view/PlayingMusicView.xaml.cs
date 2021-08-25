using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MusicPlayer.model;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicPlayer.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayingMusicView : Page
    {
        public RunDelegate runDelegate;
        Stopwatch ss = new Stopwatch { };
        DispatcherTimer secondstimer = new DispatcherTimer();
        MusicInfo music;
        int second;
        public PlayingMusicView()
        {
            this.InitializeComponent();
            #region
            // String url = $"http://api.mp3.zing.vn/api/download/audio/{music.Id}/Id/320";
            //WebClient webClient = new WebClient();
            //byte[] ABC = webClient.DownloadData(url);
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //StorageFile sampleFile = await storageFolder.CreateFileAsync("abc.mp3", CreationCollisionOption.ReplaceExisting);
            //await FileIO.WriteBytesAsync(sampleFile, );
            //WebClient webClient = new WebClient();
            //MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
            //MediaStreamSource mediaStream = new MediaStreamSource();
            //mediaPlayer.SetUriSource(new Uri(url));
            //mediaPlayer.AutoPlay = false;
            //mediaPlayer.Play();
            //randomMusic.runDelegate = OpenPlayingView;
            #endregion

            this.Loaded += PlayingMusicView_Loaded;
            secondstimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            secondstimer.Tick += Secondstimer_Tick;
        }
        private void Secondstimer_Tick(object sender, object e)
        {
            second = ss.Elapsed.Seconds;
            
           
        }

        private void PlayingMusicView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ss.Start();
            secondstimer.Start();

            //BitmapImage bitmap = new BitmapImage();
            //Uri uri = new Uri("ms-appx:///Assets/back.png");
            //bitmap.UriSource = uri;
            ////imgThum.ImageSource = bitmap;
            //btnNext.Source = bitmap;
        }
        #region
        //private async void OpenPlayingView(Object musicInfo)
        //{
        //    if (musicInfo is MusicInfo)
        //    {
        //        MusicInfo musicInfo2 = musicInfo as MusicInfo;
        //        String url = $"https://api.mp3.zing.vn/api/streaming/audio.co/{musicInfo2.Id}/320";

        //        SystemMediaTransportControls systemMediaTransportControls = SystemMediaTransportControls.GetForCurrentView();
        //        //musicControl.Source = ;
        //        MediaPlaybackItem mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
        //        MediaPlaybackList mediaPlaybackList = new MediaPlaybackList();
        //        mediaPlaybackList.Items.Add(mediaPlaybackItem);

        //    }
        //}
        #endregion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter != null)
            {
                MusicInfo musicInfo = (MusicInfo)e.Parameter;

                txtName.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                txtName.Text = musicInfo.Title;

                BitmapImage bitmap = new BitmapImage();
                Uri uri = new Uri(musicInfo.Thumbnail);
                bitmap.UriSource = uri;
                //imgThum.ImageSource = bitmap;
                imgThum.Source = bitmap;
                
                

                txtNameCasi.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
                txtNameCasi.FontFamily = new FontFamily("Arial");
                txtNameCasi.Text = musicInfo.Performer;
            }
        }
       
    }
}
