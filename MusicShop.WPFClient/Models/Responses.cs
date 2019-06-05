using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.WPFClient.Models
{
    public class SongResponse : BindableBase
    {
        private string name;
        private string author;
        private string album;
        private string image;
        private DateTime dateRelease;

        public string Name { get
            { return name; }
            set {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Author
        {
            get
            { return author; }
            set
            {
                author = value;
                RaisePropertyChanged("Author");
            }
        }
        public string Album
        {
            get
            { return album; }
            set
            {
                album = value;
                RaisePropertyChanged("Album");
            }
        }
        public string Image
        {
            get
            { return image; }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }
        public DateTime DateRelease
        {
            get
            { return dateRelease; }
            set
            {
                dateRelease = value;
                RaisePropertyChanged("Image");
            }
        }
    }
    public class PublisherResponse
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PerformerCount { get; set; }
    }
    public class AuthorResponse
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int MusicCount { get; set; }
    }

    public class GenreResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
