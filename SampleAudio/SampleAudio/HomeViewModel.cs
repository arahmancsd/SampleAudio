using MediaManager;
using MediaManager.Library;
using MediaManager.Player;
using SampleAudio.Fonts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
            PlayChapterCommand = new Command(async () => await ExecutePlayChapterCommand());

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
            try
            {
                if (CrossMediaManager.Current.State == MediaPlayerState.Paused)
                    await CrossMediaManager.Current.PlayPause();
                else if (CrossMediaManager.Current.State == MediaPlayerState.Playing)
                    await CrossMediaManager.Current.Pause();
                else
                {
                    ; 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
