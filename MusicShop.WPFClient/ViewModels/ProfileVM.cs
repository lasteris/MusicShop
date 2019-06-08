using System.Collections.ObjectModel;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class ProfileVM : BindableBase
    {
        public readonly MusicShopAPIHelper Helper;

        public ObservableCollection<SongResponse> Music { get; set; } = new ObservableCollection<SongResponse>();
        
        public ProfileVM()
        {
            Helper = new MusicShopAPIHelper();
        }

        public string Name
        {
            get { return Options.MusicOptions.User.Name; }
            set
            {
                Options.MusicOptions.User.Name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Phone
        {
            get { return Options.MusicOptions.User.Phone; }
            set
            {
                Options.MusicOptions.User.Phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        public string Login
        {
            get { return Options.MusicOptions.User.Login; }
            set
            {
                Options.MusicOptions.User.Login = value;
                RaisePropertyChanged("Login");
            }
        }



    }
}
