using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class AllMusicVM : BindableBase
    {
        MusicShopAPIHelper ext;
        public AllMusicVM()
        {
            ext = new MusicShopAPIHelper();
             LoadMusicAsync();
        }
        public ObservableCollection<SongResponse> Music { get; set; }

        private SongResponse song;

        public SongResponse Song
        {
            get { return song; }
            set
            {
                song = value;
                RaisePropertyChanged("Song");
            }
        }

        private void LoadMusicAsync()
        {
            Task.Factory.StartNew(() =>
            {
                Music = new ObservableCollection<SongResponse>(ext.GetAllMusicAsync().Result);
            }
            ).Wait();
        }
    }
}
