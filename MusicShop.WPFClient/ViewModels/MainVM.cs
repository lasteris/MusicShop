using MusicShop.WPFClient.Models;
using MusicShop.WPFClient.Services;

namespace MusicShop.WPFClient.ViewModels
{
   public class MainVM : BindableBase
    {
        private bool isVisible;
        private int index;
        public readonly WindowFactory windowFactory;
        private object currentView;
        private DelegateCommand toVewCommand;
        private DelegateCommand toLoginCommand;
        private DelegateCommand closeAppCommand;
        private DelegateCommand moveCommand;
        private DelegateCommand outCommand;
        public DelegateCommand GoToSelectedViewCommand
        {
            get
            {
                return toVewCommand ?? (toVewCommand = new DelegateCommand((index) =>
                {

                    switch (index)
                    {
                        case 0:
                            {
                                if(!(CurrentView is FirstPageVM))
                                    CurrentView = new FirstPageVM();
                                break;
                            }
                            
                        case 1:
                            {
                                if (!(CurrentView is PublisherVM))
                                    CurrentView = new PublisherVM();
                                break;
                            }
                            
                        case 2:
                            {
                                if (!(CurrentView is PerformersVM))
                                    CurrentView = new PerformersVM();
                                break;
                            }
                           
                        case 3:
                            {
                                if (!(CurrentView is AllMusicVM))
                                {
                                    //Так как мы переходим в список музыки через sidebar, выставляем true
                                    //чтобы при загрузке музыки потерлись все ранее установленные поля настроек
                                    Options.MusicOptions.IsStraight = true;
                                    CurrentView = new AllMusicVM();                                    
                                }
                                break;
                            }
                        case 4:
                            {
                                if (!(CurrentView is CartVM))
                                    CurrentView = new CartVM();
                                break;
                            }
                        case 5:
                            {
                                if (!(CurrentView is ProfileVM))
                                    CurrentView = new ProfileVM();
                                break;
                            }
                        default:
                            break;
                    }
                }, null));
            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return toLoginCommand ?? (toLoginCommand = new DelegateCommand(obj =>
                {
                    windowFactory.CreateWindow(obj.ToString()).ShowWindow();

                    if(Options.MusicOptions.User.Name != "Guest")
                    {
                        IsVisible = true;
                    }

                }, obj => Options.MusicOptions.User.Name == "Guest"));
            }
        }

        public DelegateCommand OutCommand
        {
            get
            {
                return outCommand ?? (outCommand = new DelegateCommand(obj =>
                {

                    Options.MusicOptions.User = new ClientResponse
                    {
                        Login = "Guest",
                        Password = "Guest"
                    };
                        IsVisible = false;

                }, obj => Options.MusicOptions.User.Name != "Guest"));
            }
        }
        public DelegateCommand CloseAppCommand
        {
            get
            {
                return closeAppCommand ?? (closeAppCommand = new DelegateCommand(obj =>
                {
                    if (obj is IWindow window)
                        window.CloseApp();

                }, null));
            }
        }

        public DelegateCommand MoveCommand
        {
            get
            {
                return moveCommand ?? (moveCommand = new DelegateCommand(obj =>
                {
                    if (obj is IWindow window)
                        window.MoveWindow();
                },null));
            }
        }

        public MainVM()
        {
            windowFactory = new WindowFactory();
            Options.MusicOptions.Main = this;
            CurrentView = new FirstPageVM();

            IsVisible = false;
        }


        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged("Index");
            }
        }
        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

   }
}
