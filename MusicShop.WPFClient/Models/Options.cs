using System;
using System.Collections.ObjectModel;

namespace MusicShop.WPFClient.Models
{
    public sealed class Options
    {
        private static readonly Lazy<Options> Lazy = new Lazy<Options>(() => new Options());

        public static Options MusicOptions => Lazy.Value;
        private Options()
        {
            IsStraight = false;
            IsNew = false;
            IsTop = false;
            Count = 10;
            Author = "all";
            Publisher = "all";
            Genre = "all";
            User = new ClientResponse
            {
                Name = "Guest",
                Password = "Guest"
            };
        }
        public void Clear()
        {
            IsStraight = false;
            IsNew = false;
            IsTop = false;
            Count = 10;
            Author = "all";
            Publisher = "all";
            Genre = "all";
        }
        public bool IsStraight { get; set; }
        public bool IsNew { get; set; }
        public bool IsTop { get; set; }
        public int Count { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }

        public ClientResponse User { get; set; }
        public ObservableCollection<SongResponse> Cart { get; set; } = new ObservableCollection<SongResponse>();
        public ViewModels.MainVM Main {get; set;}
    };
}
