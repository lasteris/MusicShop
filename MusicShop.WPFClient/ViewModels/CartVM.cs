using MusicShop.WPFClient.Models;
using System.Collections.ObjectModel;

namespace MusicShop.WPFClient.ViewModels
{
    public class CartVM : BindableBase
    {
        private DelegateCommand buyCommand;
        private SongResponse selectedSong;

        public readonly MusicShopAPIHelper Helper;
        public DelegateCommand BuyCommand
        {
            get
            {
                return buyCommand ?? (buyCommand = new DelegateCommand(obj =>
                {

                }, null));
            }
        }

        public SongResponse SelectedSong
        {
            get { return selectedSong; }
            set
            {
                selectedSong = value;
                RaisePropertyChanged("SelectedSong");
            }
        }
        public ObservableCollection<SongResponse> Music { get; set; } = new ObservableCollection<SongResponse>();
        public CartVM()
        {
            Helper = new MusicShopAPIHelper();
        }
    }
}
