using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using MusicPlayer.model;
using Newtonsoft.Json;
using RestSharp;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicPlayer.view
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TopZingView : Page
    {
        public List<MusicInfo> musicInfo;
        MediaPlayer mediaPlayer;
        PlayingMusicView playmusic = new PlayingMusicView();
        public RunDelegate runDelegate;
        bool isRun = true;
        public TopZingView()
        {
            this.InitializeComponent();
            musicInfo = new List<MusicInfo>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            runDelegate = (RunDelegate)e.Parameter;
        }
        private void Page_Loading(FrameworkElement sender, object args)
        {
            var client = new RestClient("https://mp3.zing.vn/xhr/chart-realtime?songId=0&videoId=0&albumId=0&chart=song&time=-1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "93e37968-e61c-16ac-2f6b-8b34932f8159");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            ResponsiveTopZing.Rootobject dataRess = JsonConvert.DeserializeObject<ResponsiveTopZing.Rootobject>(response.Content);
            foreach (ResponsiveTopZing.Song song in dataRess.data.song)
            {
                MusicInfo music = new MusicInfo();
                music.Duration = chuyendoi(song.duration);
                //if (song.position < 4)
                //{
                //    if (song.position == 1)
                //    {
                //        BitmapImage bitmap = new BitmapImage();
                //        Uri uri = new Uri("ms-appx:///Assets/top1s48.png");
                //        bitmap.UriSource = uri;
                //        //Thum.Source = bitmap;
                //        music.ThumbnailRank = bitmap.UriSource.OriginalString;
                //    }
                //    if (song.position == 2)
                //    {
                //        BitmapImage bitmap = new BitmapImage();
                //        Uri uri = new Uri("ms-appx:///Assets/top2s48.png");
                //        bitmap.UriSource = uri;
                //        //Thum.Source = bitmap;
                //        music.ThumbnailRank = bitmap.UriSource.OriginalString;
                //    }
                //    if (song.position == 3)
                //    {
                //        BitmapImage bitmap = new BitmapImage();
                //        Uri uri = new Uri("ms-appx:///Assets/top3s48.png");
                //        bitmap.UriSource = uri;
                //        //Thum.Source = bitmap;
                //        music.ThumbnailRank = bitmap.UriSource.OriginalString;
                //    }
                //}
                //else
                //{
                //    music.ThumbnailRank = null;
                //}
                music.Position = song.position;
                music.Thumbnail = song.thumbnail.Replace("w94", "w512");
                music.Performer = song.performer;
                music.Title = song.name;
                music.Name = song.artists_names;
                musicInfo.Add(music);
            }
            grViewMain.Tapped += GrViewMain_Tapped;
        }
        public string chuyendoi(int timecd)
        {
            int phut = timecd / 60;
            int giay = timecd % 60;
            string thoigian = phut + ":" + giay;
            return thoigian;
        }
        private void GrViewMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MusicInfo music = (MusicInfo)grViewMain.SelectedItem;
            runDelegate(music);
            String url = $"http://api.mp3.zing.vn/api/streaming/audio.co/{music.Id}/320";

            WebClient webClient = new WebClient();
            MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
            mediaPlayer = new MediaPlayer();
            mediaPlayer.SetUriSource(new Uri(url));

           

        }
    }
}
