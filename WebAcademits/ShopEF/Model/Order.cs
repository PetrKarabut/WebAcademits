using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEF.Model
{
    public class Order
    {
        public int Id { get; set; }

        public virtual Buyer Buyer { get; set; }

        public string Date { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
