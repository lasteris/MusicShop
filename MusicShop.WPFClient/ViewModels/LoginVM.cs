using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    class LoginVM : BindableBase
    {
        private string login;
        private string password;
        private bool isCreature;
        private DelegateCommand loginCommand;

        public DelegateCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new DelegateCommand(obj =>
                {
                    if(obj is IHavePassword havePassword)
                    {
                        var securePass = havePassword.Password;

                        //если IsCreature true, то надо отправить Post на клиента

                        if(Helper.TryLogin(Login, securePass))
                        {
                            if(obj is IWindow window)
                            {
                                window.CloseWindow();
                            }
                        }
                    }
                }, null));
            }
        }
        
        public readonly MusicShopAPIHelper Helper;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public bool IsCreature
        {
            get
            {
                return isCreature;
            }
            set
            {
                isCreature = value;
                RaisePropertyChanged("IsCreature");
            }
        }
        public LoginVM()
        {
            Helper = new MusicShopAPIHelper();
        }
    }
}
