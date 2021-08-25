using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using MusicPlayer.model;
using MusicPlayer.view;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        TopZingView topZingView = new TopZingView();
        MusicInfo musicInfo = new MusicInfo();
        MusicInfo musicInfo2;
        MediaPlaybackItem mediaPlaybackItem;
        RandomMusicView randomMusic = new RandomMusicView();
        PlayingMusicView playingMusic = new PlayingMusicView();
        bool isRun = true;
        public RunDelegate runDelegate;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            randomMusic.runDelegate = OpenPlayingView;

        }
        App app;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.ItemInvoked += NavView_ItemInvoked;
            runDelegate = OpenPlayingView;
            
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            app = (App)e.Parameter;
            app.EnteredBackground += App_EnteredBackground;
            app.LeavingBackground += App_LeavingBackground;
        }

        private void App_LeavingBackground(object sender, Windows.ApplicationModel.LeavingBackgroundEventArgs e)
        {
            //Helper.ShowToast("ok");
            
        }

        private void App_EnteredBackground(object sender, Windows.ApplicationModel.EnteredBackgroundEventArgs e)
        {
            //Helper.ShowToast("ok34tsdkfgsfgfd");
            
        }
        //private async void OpenPlayingView(MusicInfo musicInfo)
        //{
        //    String url = $"https://api.mp3.zing.vn/api/streaming/audio.co/{musicInfo.Id}/320";

        //    SystemMediaTransportControls systemMediaTransportControls = SystemMediaTransportControls.GetForCurrentView();
        //    //musicControl.Source = ;
        //    MediaPlaybackItem mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
        //    MediaPlaybackList mediaPlaybackList = new MediaPlaybackList();
        //    mediaPlaybackList.Items.Add(mediaPlaybackItem);
        //    musicControl.AreTransportControlsEnabled = true;
        //    musicControl.AutoPlay = true;
        //}
        //private async void MediaPlaybackItem_TimedMetadataTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        //{
        //    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        //    {
        //        for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
        //        {
        //            var timedMetadataTrack = sender.TimedMetadataTracks[index];

        //            ToggleButton toggle = new ToggleButton() {
        //                Content = String.IsNullOrEmpty(timedMetadataTrack.Label) ? $"Track {index}" : timedMetadataTrack.Label,
        //                Tag = (uint)index
        //            };
        //            toggle.Checked += Toggle_Checked;
        //            toggle.Unchecked += Toggle_Unchecked;

        //            //MetadataButtonPanel.Children.Add(toggle);
        //        }
        //    });
        //}

        //private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
        //    TimedMetadataTrackPresentationMode.Disabled);
        //}

        //private void Toggle_Checked(object sender, RoutedEventArgs e)
        //{
        //    mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
        //    TimedMetadataTrackPresentationMode.PlatformPresented);
        //}

        private async void OpenPlayingView(Object musicInfo)
        {
            if (musicInfo is MusicInfo)
            {
                musicInfo2 = musicInfo as MusicInfo;
                String url = $"https://api.mp3.zing.vn/api/streaming/audio.co/{musicInfo2.Id}/320";
                MediaPlayer mediaPlayer = new MediaPlayer();
                musicControl.Source = MediaSource.CreateFromUri(new Uri(url));

                #region
                //SystemMediaTransportControls systemMediaTransportControls = SystemMediaTransportControls.GetForCurrentView();
                //WebClient webClient = new WebClient();
                //MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
                //MediaStreamSource mediaStream = new MediaStreamSource();
                #endregion
                //mediaPlayer = new MediaPlayer();
                //mediaPlayer.SetUriSource(new Uri(url));

                #region
                //////musicControl.Source = ;
                /////
                ////MediaPlaybackItem mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
                ////MediaPlaybackList mediaPlaybackList = new MediaPlaybackList();
                ////mediaPlaybackList.Items.Add(mediaPlaybackItem);
                //#endregion
                //mediaPlayer.RealTimePlayback = true;
                //if (isRun == true)
                //{
                //    musicControl.MediaPlayer.AutoPlay = true;
                //    musicControl.MediaPlayer.Play();
                //    frNext.Navigate(typeof(PlayingMusicView), musicInfo2);
                //}
                //else
                //{
                //    mediaPlayer.Pause();
                //}

                //musicControl.AreTransportControlsEnabled = true;
                //musicControl.AutoPlay = false;
                //musicControl.MediaPlayer.Play();
                //frNext.Navigate(typeof(PlayingMusicView), musicInfo2);
                #endregion
            }

        }


        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.ToString().Equals("Top Zing"))
            {
                frNext.Navigate(typeof(TopZingView), runDelegate);
            }
            if (args.InvokedItem.ToString().Equals("Random music"))
            {
                frNext.Navigate(typeof(RandomMusicView), runDelegate);
            }
            if (args.InvokedItem.ToString().Equals("Playing music"))
            {
                frNext.Navigate(typeof(PlayingMusicView), musicInfo2);
            }
        }
    }
}
