using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class AllMusicVM : BindableBase
    {
        private readonly APIHelper Helper;
        private DelegateCommand addToCartCommand;

        public DelegateCommand AddToCartCommand
        {
            get
            {
                return addToCartCommand ?? (addToCartCommand = new DelegateCommand(obj =>
                {
                    if(obj is SongResponse song)
                    {
                       var check = Options.MusicOptions.Cart.Where(s => s.Identity == song.Identity).SingleOrDefault();
                        if(check is null)
                        {
                            Options.MusicOptions.Cart.Add(song);
                        }
                    }
                }, null));
            }
        }
        public AllMusicVM()
        {
            Helper = new APIHelper();

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

                Music = new ObservableCollection<SongResponse>(Helper.GetAllMusicAsync(options));
            }
            ).Wait();
        }
    }
}
