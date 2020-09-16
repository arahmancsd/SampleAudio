using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Player;
using SampleAudio.Fonts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleAudio
{
    public class TopPage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public static async Task<List<TopPage>> GetTopPages()
        {
            List<TopPage> topPages = new List<TopPage>
            {
                new TopPage() { Id = 1, Title = "Favorites" }
            };
            return await Task.FromResult(topPages);
        }
    }
    public class HomeViewModel : BaseViewModel
    {
        public ICommand LoadContentsCommand { get; set; }
        public ICommand PlayChapterCommand { get; set; }
        public ICommand TopPageCommand { get; set; }
        public HomeViewModel()
        {
            Title = "listen";
            LoadContentsCommand = new Command(async () => await ExecuteLoadContents());
            PlayChapterCommand = new Command(() =>
            Device.BeginInvokeOnMainThread(async () => {
                await ExecutePlayChapterCommand();
            }));

            CrossMediaManager.Current.MediaPlayer.ShowPlaybackControls = true;
            LoadContentsCommand.Execute(null);
        }
        ObservableCollection<TopPage> _topPages;
        public ObservableCollection<TopPage> TopPages
        {
            get { return _topPages; }
            set { SetProperty(ref _topPages, value); }
        }
        ObservableCollection<AudioReciter> _reciters;
        public ObservableCollection<AudioReciter> Reciters
        {
            get { return _reciters; }
            set { SetProperty(ref _reciters, value); }
        }
        async Task ExecuteLoadContents()
        {
            IsBusy = true;
            try
            {
                TopPages = new ObservableCollection<TopPage>(await TopPage.GetTopPages());
                Reciters = new ObservableCollection<AudioReciter>(await AudioReciter.GetRecitersAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecutePlayChapterCommand()
        {
            if (IsBusy) return; 
            IsBusy = true;
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                var reciter = Reciters.FirstOrDefault();
                
                    var mediaItem = await CrossMediaManager.Current.Play(reciter.DownloadURL);
                    CrossMediaManager.Current.Queue.Current.IsMetadataExtracted = false;
                    mediaItem.Title = reciter.Name;
                    var image = new Image() { Source = ImageSource.FromUri(new Uri(reciter.ImagePath)) };
                    CrossMediaManager.Current.Queue.Current.Title = string.Format("{0} - {1} ({2})", "1", "ChName", "English");
                    CrossMediaManager.Current.Queue.Current.Album = reciter.Name;
                    CrossMediaManager.Current.Queue.Current.Artist = reciter.Name;
                    CrossMediaManager.Current.Queue.Current.AlbumImage = image;
                    CrossMediaManager.Current.Queue.Current.AlbumImageUri = reciter.ImagePath;
                    CrossMediaManager.Current.Queue.Current.DisplayImage = image;
                    CrossMediaManager.Current.Queue.Current.DisplayImageUri = reciter.ImagePath;
                    CrossMediaManager.Current.Queue.Current.Image = image;
                    CrossMediaManager.Current.Queue.Current.ImageUri = reciter.ImagePath;
                    CrossMediaManager.Current.Notification.UpdateNotification();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally { IsBusy = false; }
        }
    }
}
