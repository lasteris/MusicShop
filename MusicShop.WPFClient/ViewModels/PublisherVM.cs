

using MusicShop.WPFClient.Models;
using System.Collections.ObjectModel;

namespace MusicShop.WPFClient.ViewModels
{
   public class PublisherVM : BindableBase
    {
        private PublisherResponse selectedPublisher;
        private DelegateCommand toPublishersAuthors;
        //private DelegateCommand toSite;
        public readonly MusicShopAPIHelper Helper;

        public DelegateCommand GoToPublishersAuthorsCommand
        {
            get
            {
                return toPublishersAuthors ?? (toPublishersAuthors = new DelegateCommand(obj =>
                {
                    if(obj is PublisherResponse publisher)
                    {
                        //предварительно очистим опции
                        Options.MusicOptions.Clear();

                        Options.MusicOptions.Publisher = publisher.Name;
                        Options.MusicOptions.Main.CurrentView = new PerformersVM();
                    }
                }, null));
            }
        }

        //public DelegateCommand GoToSite
        //{
        //    get
        //    {
        //        return toSite ?? (toSite = new DelegateCommand(obj =>
        //        {
        //            System.Diagnostics.Process.Start(obj.ToString());
        //        }, null));
        //    }
        //}

        public ObservableCollection<PublisherResponse> Publishers { get; set; } = new ObservableCollection<PublisherResponse>();
        public PublisherResponse SelectedPublisher {
            get { return selectedPublisher; }
            set {
                selectedPublisher = value;
                RaisePropertyChanged("SelectedPublisher");
            }
        }
        public PublisherVM()
        {
            Helper = new MusicShopAPIHelper();
            Publishers = new ObservableCollection<PublisherResponse>(Helper.GetAllPublishersAsync());
        }
    }
}
