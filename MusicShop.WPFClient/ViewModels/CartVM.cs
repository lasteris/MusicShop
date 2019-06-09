using MusicShop.WPFClient.Models;
using System.Collections.ObjectModel;

namespace MusicShop.WPFClient.ViewModels
{
    public class CartVM : BindableBase
    {
        private DelegateCommand buyCommand;
        private DelegateCommand clearCartCommand;
        private DelegateCommand removeFromCartCommand;
        private SongResponse selectedSong;

        public readonly APIHelper Helper;
        public DelegateCommand BuyCommand
        {
            get
            {
                return buyCommand ?? (buyCommand = new DelegateCommand(obj =>
                {
                    if (Helper.TryCreateOrder())
                        Music.Clear();

                }, null));
            }
        }
        public DelegateCommand ClearCartCommand
        {
            get
            {
                return clearCartCommand ?? (clearCartCommand = new DelegateCommand(obj =>
                {
                    Music.Clear();
                }, null));
            }
        }
        public DelegateCommand RemoveFromCartCommand
        {
            get
            {
                return removeFromCartCommand ?? (removeFromCartCommand = new DelegateCommand(obj =>
                {
                    if (obj is SongResponse song)
                    {
                        Music.Remove(song);
                    }
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
        public ObservableCollection<SongResponse> Music { get; set; }
        public CartVM()
        {
            Helper = new APIHelper();
            Music = Options.MusicOptions.Cart;
        }
    }
}
