using System.Collections.ObjectModel;
using MusicShop.WPFClient.Models;

namespace MusicShop.WPFClient.ViewModels
{
    public class FirstPageVM : BindableBase
    {
        private DelegateCommand toNewMusic;
        private DelegateCommand toTopMusic;
        private DelegateCommand toSelectedMusic;
        private GenreResponse selectedGenre;

        public APIHelper ext;

        public GenreResponse SelectedGenre
        {
            get
            {
                return selectedGenre;
            }
            set
            {
                selectedGenre = value;
                RaisePropertyChanged("SelectedGenre");
            }

        }

        public ObservableCollection<GenreResponse> Genres { get; set; }
        
        public DelegateCommand GoToNewMusicCommand { get
            {
                return toNewMusic ?? (toNewMusic = new DelegateCommand(obj =>
                {
                    //предварительно очистим опции
                    Options.MusicOptions.Clear();

                    Options.MusicOptions.IsNew = true;
                    Options.MusicOptions.Main.CurrentView = new AllMusicVM();
                }, null));
            } }

        public DelegateCommand GoToTopMusicCommand
        {
            get
            {
                return toTopMusic ?? (toTopMusic = new DelegateCommand(obj =>
                {
                    //предварительно очистим опции
                    Options.MusicOptions.Clear();

                    Options.MusicOptions.IsTop = true;
                    Options.MusicOptions.Main.CurrentView = new AllMusicVM();
                }, null));
            }
        }

        public DelegateCommand GoToSelectedMusic
        {
            get
            {
                return toSelectedMusic ?? (toSelectedMusic = new DelegateCommand(obj =>
                {
                    if(obj is GenreResponse genre)
                    {
                        //предварительно очистим опции
                        Options.MusicOptions.Clear();

                        Options.MusicOptions.Genre = genre.Name;
                        Options.MusicOptions.Main.CurrentView = new AllMusicVM();
                    }
                }));
            }
        }
        public FirstPageVM()
        {
            ext = new APIHelper();
            Genres = new ObservableCollection<GenreResponse>(ext.GetAllGenresAsync());
        }


    }
}
