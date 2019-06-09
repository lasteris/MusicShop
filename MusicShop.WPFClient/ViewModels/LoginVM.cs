using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    class LoginVM : BindableBase
    {
        private string login;
        private string password;
        private bool isCreature;
        private DelegateCommand loginCommand;
        private string name;
        private string phone;

        public DelegateCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new DelegateCommand(obj =>
                {
                    if(obj is IHavePassword havePassword)
                    {
                        var securePass = havePassword.Password;

                        if (IsCreature)
                        {
                            if(Helper.TryRegister(Login, securePass, Name, Phone))
                            {

                                if (obj is IWindow window)
                                {
                                    window.CloseWindow();
                                }
                            }

                        }
                        else
                        {
                            if (Helper.TryLogin(Login, securePass))
                            {
                                if (obj is IWindow window)
                                {
                                    window.CloseWindow();
                                }
                            }
                        }
                        
                    }
                }, null));
            }
        }
        
        public readonly APIHelper Helper;

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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                RaisePropertyChanged("Phone");
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
            Helper = new APIHelper();
        }
    }
}
