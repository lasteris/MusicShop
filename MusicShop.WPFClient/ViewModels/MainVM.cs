using MusicShop.WPFClient.Views;

namespace MusicShop.WPFClient.ViewModels
{
   public class MainVM : BindableBase
    {
        private int index;
        private object currentView;
        private DelegateCommand toVewCommand;
        public DelegateCommand GoToSelectedViewCommand
        {
            get
            {
                return toVewCommand ?? (toVewCommand = new DelegateCommand((index) =>
                {
                    switch (index)
                    {
                        case 0:
                            CurrentView = new FirstPageVM();
                            break;
                        case 1:
                            CurrentView = new PublisherVM();
                            break;
                        case 2:
                            CurrentView = new PerformersVM();
                            break;
                        case 3:
                            CurrentView = new AllMusicVM();
                            break;
                        case 4:
                            CurrentView = new CartVM();
                            break;
                        case 5:
                            CurrentView = new ProfileVM();
                            break;
                        default:
                            break;
                    }
                }, null));
            }
        }

        public MainVM()
        {
            //мне не нравится что VM знает об контроле
            CurrentView = new UserControlAllMusic();

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
        
    }
}
