using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Genre
    {
        public Genre()
        {
            Song = new HashSet<Song>();
        }

        public int GenreId { get; set; }
        public string GenreImage { get; set; }
        public string GenreName { get; set; }
        public string GenreDesc { get; set; }

        public virtual ICollection<Song> Song { get; set; }
    }
}
