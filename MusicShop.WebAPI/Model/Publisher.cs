using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Publisher
    {
        public Publisher()
        {
            Author = new HashSet<Author>();
        }

        public int PubId { get; set; }
        public string PubName { get; set; }
        public string PubWebsite { get; set; }
        public string PubImage { get; set; }

        public virtual ICollection<Author> Author { get; set; }
    }
}
