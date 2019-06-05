using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Author
    {
        public Author()
        {
            Song = new HashSet<Song>();
        }

        public int AuId { get; set; }
        public int? PubId { get; set; }
        public string AuName { get; set; }
        public string AuDesc { get; set; }
        public string AuWebsite { get; set; }
        public string AuImage { get; set; }

        public virtual Publisher Pub { get; set; }
        public virtual ICollection<Song> Song { get; set; }
    }
}
