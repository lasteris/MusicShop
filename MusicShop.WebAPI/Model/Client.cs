using System;
using System.Collections.Generic;

namespace MusicShop.WebAPI.Model
{
    public partial class Client
    {
        public Client()
        {
            PaymentCard = new HashSet<PaymentCard>();
            Purchase = new HashSet<Purchase>();
        }

        public int ClId { get; set; }
        public string ClName { get; set; }
        public string ClLogin { get; set; }
        public string ClPassword { get; set; }
        public string ClEmail { get; set; }
        public string ClPhone { get; set; }

        public virtual ICollection<PaymentCard> PaymentCard { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
