using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
  public  class PerformersVM : BindableBase
    {
        private AuthorResponse performer;
        private DelegateCommand toSelectedPerformer;
        public readonly MusicShopAPIHelper Helper;

        public DelegateCommand GoToSelectedPerformer
        {
            get
            {
                return toSelectedPerformer ?? (toSelectedPerformer = new DelegateCommand(obj =>
                {
                    if(obj is AuthorResponse author)
                    {
                        //предварительно очистим опции
                        Options.MusicOptions.Clear();

                        Options.MusicOptions.Author = author.Name;
                        Options.MusicOptions.Main.CurrentView = new AllMusicVM();
                    }
                    
                }, null));
            }
        }

        public AuthorResponse SelectedPerformer {
            get
            {
                return performer;
            }
            set
            {
                performer = value;
                RaisePropertyChanged("SelectedPerformer");
            }
        }
        public ObservableCollection<AuthorResponse> Performers { get; set; } = new ObservableCollection<AuthorResponse>();

        public PerformersVM()
        {
            Helper = new MusicShopAPIHelper();
            Performers = new ObservableCollection<AuthorResponse>(Helper.GetAllPerformersAsync(Options.MusicOptions));
        }
    }
}
