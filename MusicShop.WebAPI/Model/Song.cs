using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Song
    {
        public Song()
        {
            Includes = new HashSet<Includes>();
        }

        public int SongId { get; set; }
        public int? GenreId { get; set; }
        public int AuId { get; set; }
        public int AlId { get; set; }
        public string SongName { get; set; }
        public double? Price { get; set; }
        public string Lyrics { get; set; }
        public double Duration { get; set; }
        public string License { get; set; }

        public virtual Album Al { get; set; }
        public virtual Author Au { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Includes> Includes { get; set; }
    }
}
