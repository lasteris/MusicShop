using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Album
    {
        public Album()
        {
            Song = new HashSet<Song>();
        }

        public int AlId { get; set; }
        public DateTime AlDateRelease { get; set; }
        public string AlDesc { get; set; }
        public string AlName { get; set; }
        public string AlImage { get; set; }

        public virtual ICollection<Song> Song { get; set; }
    }
}
