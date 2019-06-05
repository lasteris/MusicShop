using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class PaymentCard
    {
        public int ClId { get; set; }
        public int PayId { get; set; }
        public string PayNumber { get; set; }
        public string PayHolder { get; set; }
        public DateTime PayExpireDate { get; set; }
        public string PayCvc { get; set; }

        public virtual Client Cl { get; set; }
    }
}
