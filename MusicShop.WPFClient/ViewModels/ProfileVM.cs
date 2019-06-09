using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class ProfileVM : BindableBase
    {
        private DelegateCommand saveChangesCommand;
        private DelegateCommand playCommand;
        private DelegateCommand stopCommand;
        public readonly APIHelper Helper;
        public DelegateCommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand ?? (saveChangesCommand = new DelegateCommand(obj =>
                {
                    if(obj is IHavePassword havePassword)
                    {
                            Helper.TryChangeProfile(Login, havePassword.Password, Name, Phone, Email);

                    }
                }, (obj) => (obj as IHavePassword).Password.Length > 0 ));
            }
        }
        public DelegateCommand PlayCommand
        {
            get
            {
                return playCommand ?? (playCommand = new DelegateCommand(obj =>
                {
                    if(obj is SongResponse song)
                    {
                        Helper.Play(song);
                    }
                }, null));
            }
        }
        public DelegateCommand StopCommand
        {
            get
            {
                return stopCommand ?? (stopCommand = new DelegateCommand(obj =>
                {
                        Helper.Stop();
                }, null));
            }
        }

        public ObservableCollection<SongResponse> Music { get; set; } = new ObservableCollection<SongResponse>();
        
        public ProfileVM()
        {
            Helper = new APIHelper();
            LoadMusicAsync();
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
        public string Email
        {
            get { return Options.MusicOptions.User.Email; }
            set
            {
                Options.MusicOptions.User.Email = value;
                RaisePropertyChanged("Login");
            }
        }

        private void LoadMusicAsync()
        {
                Music = new ObservableCollection<SongResponse>(Helper.GetHistoryAsync());
        }
    }
}
