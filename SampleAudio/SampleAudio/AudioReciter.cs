using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace SampleAudio
{
    public enum ReciterType
    {
        TranslationOnly = 0,
        ListenOnly = 1,
        Both = 2,
    }
    public class AudioReciter
    {
        public int ReciterID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string DownloadURL { get; set; }
        public string ImagePath { get; set; }
        public ReciterType ReciterType { get; set; }

        readonly static List<AudioReciter> listOfReciters = new List<AudioReciter>
            {
                new AudioReciter() { ReciterID = 1, Name = "ABC", ImagePath = "https://www.azee.tech/uz.png",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/001.mp3", ReciterType = ReciterType.ListenOnly },
                new AudioReciter() { ReciterID = 1, Name = "BDC", ImagePath = "https://www.azee.tech/r9.jpg",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/114.mp3", ReciterType =  ReciterType.ListenOnly },
                new AudioReciter() { ReciterID = 1, Name = "AEC", ImagePath = "https://www.azee.tech/r9.png",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/113.mp3", ReciterType =  ReciterType.ListenOnly },
                new AudioReciter() { ReciterID = 1, Name = "Name", ImagePath = "https://www.azee.tech/r1.png",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/112.mp3", ReciterType =  ReciterType.ListenOnly },
                new AudioReciter() { ReciterID = 1, Name = "XYZ", ImagePath = "https://www.azee.tech/r5.png",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/113.mp3", ReciterType =  ReciterType.ListenOnly },
                new AudioReciter() { ReciterID = 1, Name = "Iphone", ImagePath = "https://www.azee.tech/r6.png",
                    DownloadURL = "https://www.azee.tech/MaherAlMuaiqly64kbps/110.mp3", ReciterType =  ReciterType.ListenOnly },
            };
        public static async Task<AudioReciter> GetReciterAsync()
        {
            return await Task.FromResult(listOfReciters.FirstOrDefault());
        }
        public static async Task<List<AudioReciter>> GetRecitersAsync()
        {
            return await Task.FromResult(listOfReciters);
        }
    }
}