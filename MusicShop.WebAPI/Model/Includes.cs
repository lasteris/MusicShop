using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Includes
    {
        public int SongId { get; set; }
        public int ClId { get; set; }
        public int PurId { get; set; }

        public virtual Purchase Purchase { get; set; }
        public virtual Song Song { get; set; }
    }
}
