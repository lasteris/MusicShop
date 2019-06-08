using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class AllMusicVM : BindableBase
    {
        private readonly MusicShopAPIHelper ext;
        public AllMusicVM()
        {
            ext = new MusicShopAPIHelper();

            LoadMusicAsync(Options.MusicOptions);
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

        private void LoadMusicAsync(Options options)
        {
            Task.Run(() =>
            {
                if (Options.MusicOptions.IsStraight)
                    Options.MusicOptions.Clear();

                Music = new ObservableCollection<SongResponse>(ext.GetAllMusicAsync(options));
            }
            ).Wait();
        }
    }
}
