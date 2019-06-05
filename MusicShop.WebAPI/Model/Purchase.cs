using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Purchase
    {
        public Purchase()
        {
            Includes = new HashSet<Includes>();
        }

        public int ClId { get; set; }
        public int PurId { get; set; }
        public DateTime? PurDate { get; set; }
        public double? TotalSum { get; set; }
        public int? TotalQty { get; set; }

        public virtual Client Cl { get; set; }
        public virtual ICollection<Includes> Includes { get; set; }
    }
}
