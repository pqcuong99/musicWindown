using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using MusicPlayer.model;
using Newtonsoft.Json;
using RestSharp;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicPlayer.view
{
    public delegate void RunDelegate(Object data);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RandomMusicView : Page
    {
        public List<MusicInfo> musicInfos;
        MediaPlayer mediaPlayer;
        MediaPlaybackItem mediaPlaybackItem;
        PlayingMusicView playmusic;
        MediaPlaybackList mediaPlaybackList;
        String url;
        public RunDelegate runDelegate;
        bool isRun = true;
        public RandomMusicView()
        {
            this.InitializeComponent();
            musicInfos = new List<MusicInfo>();
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

                music.Id = song.id;
                music.Thumbnail = song.thumbnail.Replace("w94", "w512");
                music.Performer = song.performer;
                music.Title = song.name;
                music.Name = song.artists_names;
                musicInfos.Add(music);

                url = $"http://api.mp3.zing.vn/api/streaming/audio.co/{music.Id}/320";

                WebClient webClient = new WebClient();
                MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
                //MediaStreamSource mediaStream = new MediaStreamSource();

                mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
                mediaPlaybackList = new MediaPlaybackList();
                mediaPlaybackList.Items.Add(mediaPlaybackItem);
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
        private async void GrViewMain_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MusicInfo music = (MusicInfo)grViewMain.SelectedItem;
            runDelegate(music);
            #region
            //String url = $"http://api.mp3.zing.vn/api/streaming/audio.co/{music.Id}/320";

            //WebClient webClient = new WebClient();
            //MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
            ////MediaStreamSource mediaStream = new MediaStreamSource();



            //mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
            //mediaPlaybackList = new MediaPlaybackList();
            //mediaPlaybackList.Items.Add(mediaPlaybackItem);

            //mediaPlayer = new MediaPlayer();
            //mediaPlayer.SetUriSource(new Uri(url));
            //runDelegate(mediaPlaybackList);
            //if (isRun == true)
            //{
            //    //mediaPlayer.AutoPlay = false;
            //    //mediaPlayer.Play();
            //    isRun = false;
            //}
            //else
            //{
            //    mediaPlayer.Pause();

            //    isRun = false;
            //}
            #endregion

        }

    }
}
